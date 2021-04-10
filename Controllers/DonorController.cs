using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeraldtonHospital_v1.Models;
using GeraldtonHospital_v1.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace GeraldtonHospital_v1.Controllers
{
    public class DonorController : Controller
    {
        //Http Client is the proper way to connect to a webapi: 

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client; 

        static DonorController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                //Gets or sets a value that indicates whether the handler should follow redirection responses.
                //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclienthandler?view=net-5.0
                AllowAutoRedirect = false
            };

            //get info from database: 
            client = new HttpClient(handler);
            //===========================================================
            //======add your localhost port number !!!====================
            client.BaseAddress = new Uri("https://localhost:44315/api/");
            //MediaTypeQualityHeaderValue provides support for the media type and quality in a Content-Type header : json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }

        // GET: Donor/List
        public ActionResult List()
        {
            ListDonors ViewModel = new ListDonors();
            
            string url = "donordata/getdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DonorDto> SelectedDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
                ViewModel.Donors = SelectedDonors;
                return View(ViewModel);
            }
            else
            {
                //Redirects to the specified action using the action name.
                return RedirectToAction("Error");
            }
        }

        // GET: Donor/Details/5
        public ActionResult Details(int id)
        {
            ShowDonor ViewModel = new ShowDonor();
            string url = "donordata/finddoor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine(response.StatusCode); 
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor Data transfer object - see Donor Model for more details
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                ViewModel.Donor = SelectedDonor;

                //find donations provided by donor
                //no need to throw error if null
                url = "donordata/getdonationsfordonor/" + id;
                response = client.GetAsync(url).Result;
                //catches status code (200 OK, 301 REDIRECT) ... 
                //Debug.WriteLine(response.StatusCode);

                //Put data into Donor DTO (data transfer object)
                IEnumerable<DonationDto> SelectedDonations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
                ViewModel.DonorDonations = SelectedDonations;

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
            //get info about donations this donor has made
            string url = "donordata/getdonor";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<DonationDto> PotentialDonations = response.Content.ReadAsync<IEnumerable<DonorDto>>().Result;

            //ViewModel.Alldonations = PotentialDonations; 

            return View(ViewModel);
        }

        // POST: Donor/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult Create(Donor DonorInfo) //Donorinfo is a parameter. not part of DB
        {
            //Debug.WriteLine(DonorInfo.FirstName); 
            string url = "donordata/adddonor";
            Debug.WriteLine(jss.Serialize(DonorInfo));
            HttpContent content = new StringContent(jss.Serialize(DonorInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int Donorid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = Donorid });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }
        [HttpGet]
        // GET: Donor/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDonor ViewModel = new UpdateDonor();

            string url = "donordata/finddonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                ////Put data into Donor data transfer object
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                ViewModel.donor = SelectedDonor;

                //get information about donation this Donor has made: 
                url = "donationdata/getdonationsfordonor/" + id;
                response = client.GetAsync(url).Result;

                //Put data into Donation DTO (data transfer object
                IEnumerable<DonationDto> SelectedDonations= response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
                ViewModel.donordonations = SelectedDonations;

                return View(ViewModel);
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
            //Debug.WriteLine(DonorInfo.UserName);
            string url = "donordata/updatedonor/" + id;
            //Debug.WriteLine(jss.Serialize(DonorInfo));
            HttpContent content = new StringContent(jss.Serialize(DonorInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //GET: Sponsor/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "donordata/finddonor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status cod (200 OK, 301 REDIRECT), etc
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor data transfer object
                DonorDto SelectedDonor = response.Content.ReadAsSync<DonorDto>().Result;
                return View(SelectedDonor);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // POST: Donor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "donordata/deletedonor/" + id;
            //need to post something - so send post body empty ""
            //is it possible to add id? (id) - test what happens
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
