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
    public class DonationController : Controller
    {
        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static DonationController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44334/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }



        // GET: Donation/List
        public ActionResult List()
        {
            string url = "donationdata/getdonations";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DonationDto> SelectedDonations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;
                return View(SelectedDonations);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            ShowDonor ViewModel = new ShowDonor();
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donation data transfer object
                //DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                //ViewModel.Donation = SelectedDonation;


                url = "donationdata/finddonorfordonation/" + id;
                response = client.GetAsync(url).Result;
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                ViewModel.Donor = SelectedDonor;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Donation/Create
        public ActionResult Create()
        {
            UpdateDonor ViewModel = new UpdateDonor();
            //get information about Donors this Donation COULD play for.
            string url = "donordata/getdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<DonorDto> PotentialDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
            //ViewModel.allDonors = PotentialDonors;

            return View(ViewModel);
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Donation DonationInfo)
        {
            //Debug.WriteLine(DonationInfo.DonationAmount);
            string url = "donationdata/adddonation";
            Debug.WriteLine(jss.Serialize(DonationInfo));
            HttpContent content = new StringContent(jss.Serialize(DonationInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int Donationid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = Donationid });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDonor ViewModel = new UpdateDonor();

            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donation data transfer object
                //DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                //ViewModel.Donation = SelectedDonation;

                //get information about Donors and donation info.
                //url = "donordata/getdonors";
                //response = client.GetAsync(url).Result;
                //IEnumerable<DonorDto> PotentialDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
                //ViewModel.alldonors = PotentialDonors;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Donation DonationInfo, HttpPostedFileBase DonationPic)
        //Donation pic matches with the view in the edit.cshtml name="DonationPic"
        //
        {
            //Debug.WriteLine(DonationInfo.DonationAmount); //DEBUG
            string url = "donationdata/UpdateDonor/" + id;
            //Debug.WriteLine(jss.Serialize(DonationInfo));
            HttpContent content = new StringContent(jss.Serialize(DonationInfo));
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

        // GET: Donation/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Donationdata/findDonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donation data transfer object
                DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                return View(SelectedDonation);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "donationdata/deletedonation/" + id;
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
