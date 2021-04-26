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
    public class reviewController : Controller
    {


        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        /// <summary>
        /// This allows us to access a pre-defined C# HttpClient 'client' variable for sending POST and GET requests to the data access layer.
        /// </summary>
        static reviewController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44319/api/");
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


        
        // GET: Review/List?{PageNum}
        // If the page number is not included, set it to 0
        public ActionResult List(int PageNum = 0)
        {

            ListReviews ViewModel = new ListReviews();
            ViewModel.isadmin = User.IsInRole("Admin");


            // Grab all doctors
            string url = "reviewData/Getreviews";
            // Send off an HTTP request
            // GET : /api/doctorData/Getdoctors
            // Retrieve response
            HttpResponseMessage response = client.GetAsync(url).Result;
            // If the response is a success, proceed
            if (response.IsSuccessStatusCode)
            {
                // Fetch the response content into IEnumerable<PlayerDto>
                IEnumerable<reviewDto> SelectedReviews = response.Content.ReadAsAsync<IEnumerable<reviewDto>>().Result;

                // -- Start of Pagination Algorithm --

                // Find the total number of doctors
                int ReviewCount = SelectedReviews.Count();
                // Number of doctors to display per page
                int PerPage = 4;

                // Determines the maximum number of pages (rounded up), assuming a page 0 start.
                int MaxPage = (int)Math.Ceiling((decimal)ReviewCount / PerPage) - 1;

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
                url = "reviewData/getreviewspage/" + StartIndex + "/" + PerPage;
                response = client.GetAsync(url).Result;

                // Retrieve the response of the HTTP Request
                IEnumerable<reviewDto> SelectedReviewsPage = response.Content.ReadAsAsync<IEnumerable<reviewDto>>().Result;

                ViewModel.reviews = SelectedReviewsPage;

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
        /// This is called by the List html page, when the user clicks on any of the review.
        /// </summary>
        /// <param name="id">The review's ID.</param>
        /// <returns></returns>
        // GET: Review/Details/5
        public ActionResult Details(int id)
        {

            ShowReview ViewModel = new ShowReview();

            //Pass along to the view information about who is logged in
            ViewModel.isadmin = User.IsInRole("Admin");

            string url = "reviewData/FindReview/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into player data transfer object
                reviewDto SelectedReview = response.Content.ReadAsAsync<reviewDto>().Result;
                ViewModel.review = SelectedReview;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }




        // GET: review
        public ActionResult Index()
        {
            return View();
        }



        // GET: review/Create
        public ActionResult Create()
        {
            //GetApplicationCookie();
            UpdateReview ViewModel = new UpdateReview();

            return View(ViewModel);
        }

        // POST: review/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(review ReviewInfo)
        {
            GetApplicationCookie();

            Debug.WriteLine(ReviewInfo.reviewID);
            string url = "reviewdata/addReview";
            Debug.WriteLine(jss.Serialize(ReviewInfo));
            HttpContent content = new StringContent(jss.Serialize(ReviewInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int Reviewid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = Reviewid });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }


        // GET: Review/DeleteConfirm/5
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "reviewdata/findreview/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Team data transfer object
                reviewDto SelectedReview = response.Content.ReadAsAsync<reviewDto>().Result;
                return View(SelectedReview);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Team/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();
            string url = "teamdata/deletereview/" + id;
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



    }
}
