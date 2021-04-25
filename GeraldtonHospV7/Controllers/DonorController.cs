using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeraldtonHospV7.Models;
using GeraldtonHospV7.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace GeraldtonHospV7.Controllers
{
    public class DonorController : Controller
    {

        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static DonorController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
                //cookies are manually set in RequestHeader
                //UseCookies = false; 
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44317/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }

        /// <summary>
        /// Gets the authentication credientials which are sent to the Controller.
        /// NOT proper authentication technique for the WebAPI. It piggybacks 
        /// the existing authentication set up in the template for Individual 
        /// User Accounts. Considering the existing scope and complexity of the course, 
        /// it works for now.
        /// /// Here is a descriptive article which walks through the process of setting up authorization/authentication directly.
        /// https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/individual-accounts-in-web-api
        /// </summary>
        /// <returns></returns>


        //private void GetApplicationCookie()
        //{
        //    string token = "";
        //    //HTTP client is set up to be reused, otherwise it will exhaust server resources.
        //    //This is a bit dangerous because a previously authenticated cookie could be cached for
        //    //a follow-up request from someone else. Reset cookies in HTTP client before grabbing a new one.
        //    client.DefaultRequestHeaders.Remove("Cookie");
        //    if (!User.Identity.IsAuthenticated) return;
        //    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
        //    if (cookie != null) token = cookie.Value;

        //    //collect token as it is submitted to the controller
        //    //use it to pass along to the WebAPI.
        //    Debug.WriteLine("Token Submitted is : " + token);
        //    if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

        //    return;
        //}


        // GET: Donor/List
        public ActionResult List()
        {
            string url = "donordata/getdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DonorDto> SelectedDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
                return View(SelectedDonors);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donor/Details/5
        public ActionResult Details(int id)
        {
            ShowDonor ViewModel = new ShowDonor();
            string url = "Donordata/findDonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor data transfer object
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                ViewModel.Donor = SelectedDonor;

                //We don't need to throw any errors if this is null
                //A Donor not having any Donationss is not an issue.
                url = "Donordata/getDonationforDonor/" + id;
                response = client.GetAsync(url).Result;
                //Can catch the status code (200 OK, 301 REDIRECT), etc.
                //Debug.WriteLine(response.StatusCode);
                IEnumerable<DonationDto> SelectedDonationss = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
                ViewModel.DonorDonations = SelectedDonationss;


                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donor/Create
        public ActionResult Create()
        {
            UpdateDonor ViewModel = new UpdateDonor();
            //get information about resources this coder may have
            string url = "donationdata/getdonation";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DonationDto> PotentialDonations = response.Content.ReadAsAsync < IEnumerable<DonationDto>>().Result;

            ViewModel.AllDonations = PotentialDonations;


            return View(ViewModel);
        }

        // POST: Donor/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Donor DonorInfo) //DonorInfo is a paramter and not part of the database
        {
            Debug.WriteLine(DonorInfo.FirstName); //Donor first name 
            string url = "donordata/adddonor";
            Debug.WriteLine(jss.Serialize(DonorInfo));
            HttpContent content = new StringContent(jss.Serialize(DonorInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int DonorID = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = DonorID });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donor/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "Donordata/findDonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor data transfer object
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                return View(SelectedDonor);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Donor DonorInfo)
        {
            Debug.WriteLine(DonorInfo.FirstName);
            string url = "Donordata/updateDonor/" + id;
            Debug.WriteLine(jss.Serialize(DonorInfo));
            HttpContent content = new StringContent(jss.Serialize(DonorInfo));
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

        // GET: Donor/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Donordata/findDonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor data transfer object
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                return View(SelectedDonor);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            string url = "Donordata/deleteDonor/" + id;
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
