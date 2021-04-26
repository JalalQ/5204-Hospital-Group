using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using team2Geraldton.Models;
using team2Geraldton.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace team2Geraldton.Controllers
{
    public class StaffController : Controller
    {
        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static StaffController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44346/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }


        /// <summary>
        /// Grabs the authentication credentials which are sent to the Controller.
        /// This is NOT considered a proper authentication technique for the WebAPI. It piggybacks the existing authentication set up in the template for Individual User Accounts. Considering the existing scope and complexity of the course, it works for now.
        /// 
        /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        private void GetApplicationCookie()
        {
            string token = "";
            //HTTP client is set up to be reused, otherwise it will exhaust server resources.
            //This is a bit dangerous because a previously authenticated cookie could be cached for
            //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            //collect token as it is submitted to the controller
            //use it to pass along to the WebAPI.
            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }





        // GET: Staff/List
        public ActionResult List(int PageNum = 0)
        {

            ListStaffs ViewModel = new ListStaffs();
            ViewModel.isadmin = User.IsInRole("Admin");
            string url = "StaffData/GetStaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<StaffDto> SelectedStaffs = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;

                int StaffCount = SelectedStaffs.Count();
                // Number of staffs to display per page
                int PerPage = 6;
                // Determines the maximum number of pages (rounded up), assuming a page 0 start.
                int MaxPage = (int)Math.Ceiling((decimal)StaffCount / PerPage) - 1;

                // Lower boundary for Max Page
                if (MaxPage < 0) MaxPage = 0;
                // Lower boundary for Page Number
                if (PageNum < 0) PageNum = 0;
                // Upper Bound for Page Number
                if (PageNum > MaxPage) PageNum = MaxPage;

                // The Record Index of our Page Start
                int StartIndex = PerPage * PageNum;

                //Helps us generate the HTML which shows "Page 1 of ..." on the list view
                ViewData["PageNum"] = PageNum;
                ViewData["PageSummary"] = " " + (PageNum + 1) + " of " + (MaxPage + 1) + " ";

                // -- End of Pagination Algorithm --


                // Send back another request to get players, this time according to our paginated logic rules
                // GET api/staffdata/getstaffspage/{startindex}/{perpage}
                url = "Staffdata/getStaffspage/" + StartIndex + "/" + PerPage;
                response = client.GetAsync(url).Result;

                // Retrieve the response of the HTTP Request
                IEnumerable<StaffDto> SelectedStaffsPage = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;

                ViewModel.staffs = SelectedStaffsPage;

                //Return the paginated of players instead of the entire list
                return View(ViewModel);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
                return RedirectToAction("Error");
            }


        }




        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {

            ShowStaff ViewModel = new ShowStaff();
            string url = "Staffdata/findStaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
                ViewModel.staff = SelectedStaff;


                url = "staffdata/finddepartmentforstaff/" + id;
                response = client.GetAsync(url).Result;
                DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
                ViewModel.department = SelectedDepartment;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }




        //GET: Staff/Edit/2
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            UpdateStaff ViewModel = new UpdateStaff();

            string url = "Staffdata/findStaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Staff data transfer object
                StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
                ViewModel.staff = SelectedStaff;

                //get information about teams this player COULD play for.
                url = "departmentdata/getdepartments";
                response = client.GetAsync(url).Result;
                IEnumerable<DepartmentDto> PotentialDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
                ViewModel.alldepartments = PotentialDepartments;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST:Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Staff StaffInfo)
        {
            Debug.WriteLine(StaffInfo.FirstName);
            string url = "Staffdata/updateStaff/" + id;
            Debug.WriteLine(jss.Serialize(StaffInfo));
            HttpContent content = new StringContent(jss.Serialize(StaffInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }




        // GET: Staff/Create
        // only administrators get to this page
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            UpdateStaff ViewModel = new UpdateStaff();
            //get information about teams this player COULD play for.
            string url = "departmentdata/getdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> PotentialDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.alldepartments = PotentialDepartments;

            return View(ViewModel);
        }

        
        // POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Staff StaffInfo)
        {
            //pass along authentication credential in http request
            //GetApplicationCookie();

            //Debug.WriteLine(StaffInfo.FirstName);
            Debug.WriteLine(jss.Serialize(StaffInfo));
            string url = "Staffdata/addStaff";
            HttpContent content = new StringContent(jss.Serialize(StaffInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int staffid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = staffid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Staff/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Staffdata/findStaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Customer data transfer object
                StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
                return View(SelectedStaff);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Staff/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            string url = "staffdata/deleteStaff/" + id;
            //post body is empty
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}


