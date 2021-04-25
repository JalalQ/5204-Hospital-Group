using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GeraldtonHospV7.Models;
using GeraldtonHospV7.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Configuration;

namespace GeraldtonHospV7.Controllers
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
            client.BaseAddress = new Uri("https://localhost:44317/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

        }

        //STRIPE integration - basic 
        //https://www.thecodehubs.com/stripe-payment-integration-in-asp-net-mvc/?unapproved=769&moderation-hash=2f40b7153c849ad9e3ae74d57fac1c6e#comment-769

        //public ActionResult Index()
        //{
        //    ViewBag.StripePublishKey = ConfigurationManager.AppSettings["stripePublishableKey"];
        //    return View();
        //}

        //[HttpPost]
       
        //public ActionResult Charge(string stripeToken, string stripeEmail)
        //{
        //    Stripe.StripeConfiguration.SetApiKey("");  //add api key
        //    Stripe.StripeConfiguration.ApiKey = ""; //add api key

        //    var myCharge = new Stripe.ChargeCreateOptions();
        //    // always set these properties
        //    myCharge.Amount = 500;
        //    myCharge.Currency = "USD";
        //    myCharge.ReceiptEmail = stripeEmail;
        //    myCharge.Description = "Sample Charge";
        //    myCharge.Source = stripeToken;
        //    myCharge.Capture = true;
        //    var chargeService = new Stripe.ChargeService();
        //    Charge stripeCharge = chargeService.Create(myCharge);
        //    return View();
        //}

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


        //// GET: Donation/List?{PageNum}
        ////If the page number is not included, set it to 0
        //[HttpGet]
        //public ActionResult List(int PageNum=0)
        //{

        //    // Grab all Donations
        //    string url = "Donationdata/getDonations";
        //    // Send off an HTTP request
        //    // GET : /api/Donationdata/getDonations
        //    // Retrieve response
        //    HttpResponseMessage response = client.GetAsync(url).Result;
        //    // If the response is a success, proceed
        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Fetch the response content into IEnumerable<DonationDto>
        //        IEnumerable<DonationDto> SelectedDonations = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

        //        // -- Start of Pagination Algorithm --

        //        // Find the total number of Donations
        //        int DonationCount = SelectedDonations.Count();
        //        // Number of Donations to display per page
        //        int PerPage = 8;
        //        // Determines the maximum number of pages (rounded up), assuming a page 0 start.
        //        int MaxPage = (int)Math.Ceiling((decimal)DonationCount / PerPage) - 1;

        //        // Lower boundary for Max Page
        //        if (MaxPage < 0) MaxPage = 0;
        //        // Lower boundary for Page Number
        //        if (PageNum < 0) PageNum = 0;
        //        // Upper Bound for Page Number
        //        if (PageNum > MaxPage) PageNum = MaxPage;

        //        // The Record Index of our Page Start
        //        int StartIndex = PerPage * PageNum;

        //        //Helps us generate the HTML which shows "Page 1 of ..." on the list view
        //        ViewData["PageNum"] = PageNum;
        //        ViewData["PageSummary"] = " " + (PageNum + 1) + " of " + (MaxPage + 1) + " ";

        //        // -- End of Pagination Algorithm --


        //        // Send back another request to get Donations, this time according to our paginated logic rules
        //        // GET api/Donationdata/getDonationspage/{startindex}/{perpage}
        //        url = "Donationdata/getDonationspage/" + StartIndex + "/" + PerPage;
        //        response = client.GetAsync(url).Result;

        //    // Retrieve the response of the HTTP Request
        //    https://localhost:44317/Donation/ListIEnumerable<DonationDto> SelectedDonationsPage = response.Content.ReadAsAsync<IEnumerable<DonationDto>>().Result;

        //        //Return the paginated of Donations instead of the entire list
        //        return View(SelectedDonationsPage);

        //    }
        //    else
        //    {
        //        // If we reach here something went wrong with our list algorithm
        //        return RedirectToAction("Error");
        //    }

        //}



        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            ShowDonation ViewModel = new ShowDonation();
            string url = "donationdata/finddonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donation data transfer object
                DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                ViewModel.Donation = SelectedDonation;

                url = "donationdata/finddonationfordonor/" + id;
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
            UpdateDonation ViewModel = new UpdateDonation();
            //get information about Donors this Donation COULD play for.
            string url = "Donordata/getDonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DonorDto> PotentialDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
            ViewModel.Donors = PotentialDonors;

            return View(ViewModel);
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Donation DonationInfo)
        {
            Debug.WriteLine(DonationInfo.Amount);
            string url = "Donationdata/addDonation";
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
            UpdateDonation ViewModel = new UpdateDonation();

            string url = "Donationdata/findDonation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donation data transfer object
                DonationDto SelectedDonation = response.Content.ReadAsAsync<DonationDto>().Result;
                ViewModel.Donation = SelectedDonation;

                //get information about Donors this Donation COULD play for.
                url = "Donordata/getDonors";
                response = client.GetAsync(url).Result;
                IEnumerable<DonorDto> PotentialDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
                ViewModel.Donors = PotentialDonors; //refers to IEnumerable DonorDto Donors

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Donation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Donation DonationInfo)
        {
           //Debug.WriteLine(DonationInfo.DonationFirstName);
            string url = "Donationdata/updateDonation/" + id;
            Debug.WriteLine(jss.Serialize(DonationInfo));
            HttpContent content = new StringContent(jss.Serialize(DonationInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //Debug.WriteLine(response.StatusCode);

            return RedirectToAction("Details", new { id = id });
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            string url = "Donationdata/deleteDonation/" + id;
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
