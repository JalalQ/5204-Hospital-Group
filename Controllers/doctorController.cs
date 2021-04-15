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
    public class doctorController : Controller
    {

        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        /// <summary>
        /// This allows us to access a pre-defined C# HttpClient 'client' variable for sending POST and GET requests to the data access layer.
        /// </summary>
        static doctorController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44343/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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


        // GET: Doctor/List?{PageNum}
        // If the page number is not included, set it to 0
        public ActionResult List(int PageNum = 0)
        {

            ListDoctors ViewModel = new ListDoctors();
            ViewModel.isadmin = User.IsInRole("Admin");


            // Grab all doctors
            string url = "doctorsData/Getdoctors";
            // Send off an HTTP request
            // GET : /api/doctorsData/Getdoctors
            // Retrieve response
            HttpResponseMessage response = client.GetAsync(url).Result;
            // If the response is a success, proceed
            if (response.IsSuccessStatusCode)
            {
                // Fetch the response content into IEnumerable<PlayerDto>
                IEnumerable<doctorDto> SelectedDoctors = response.Content.ReadAsAsync<IEnumerable<doctorDto>>().Result;

                // -- Start of Pagination Algorithm --
                
                // Find the total number of doctors
                int DoctorCount = SelectedDoctors.Count();
                // Number of doctors to display per page
                int PerPage = 8;

                // Determines the maximum number of pages (rounded up), assuming a page 0 start.
                int MaxPage = (int)Math.Ceiling((decimal)DoctorCount / PerPage) - 1;

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
                // GET api/playerdata/getplayerspage/{startindex}/{perpage}
                url = "doctorsData/getdoctorspage/" + StartIndex + "/" + PerPage;
                response = client.GetAsync(url).Result;

                // Retrieve the response of the HTTP Request
                IEnumerable<doctorDto> SelectedDoctorsPage = response.Content.ReadAsAsync<IEnumerable<doctorDto>>().Result;

                ViewModel.doctors = SelectedDoctorsPage;

                //Return the paginated of players instead of the entire list
                return View(ViewModel);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
                return RedirectToAction("Error");
            }


        }

        /// <summary>
        /// This is called by the List View
        /// </summary>
        /// <param name="id">The doctor's ID.</param>
        /// <returns></returns>
        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {

            ShowDoctor ViewModel = new ShowDoctor();

            //Pass along to the view information about who is logged in
            ViewModel.isadmin = User.IsInRole("Admin");

            string url = "doctorsData/FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into player data transfer object
                doctorDto SelectedDoctor = response.Content.ReadAsAsync<doctorDto>().Result;
                ViewModel.doctor = SelectedDoctor;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // POST: Doctor/Create
        [HttpPost]
        //[ValidateAntiForgeryToken()]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(doctor DoctorInfo)
        {
            //pass along authentication credential in http request
            GetApplicationCookie();

            //Debug.WriteLine(PlayerInfo.PlayerFirstName);
            //Debug.WriteLine(jss.Serialize(PlayerInfo));
            string url = "doctorsdata/AddDoctor";
            HttpContent content = new StringContent(jss.Serialize(DoctorInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int doctorID = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = doctorID });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: doctor
        public ActionResult Index()
        {
            return View();
        }


        // GET: doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        /*
        // POST: doctor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
