using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using News_Events_Payments.Models;
using News_Events_Payments.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace News_Events_Payments.Controllers
{
    public class News_events1Controller : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static News_events1Controller()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                //cookies are manually set in RequestHeader
                UseCookies = false
            };

            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44395/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void GetApplicationCookie()
        {
            string token = "";
            client.DefaultRequestHeaders.Remove("Cookie");
            if (!User.Identity.IsAuthenticated) return;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(".AspNet.ApplicationCookie");
            if (cookie != null) token = cookie.Value;

            Debug.WriteLine("Token Submitted is : " + token);
            if (token != "") client.DefaultRequestHeaders.Add("Cookie", ".AspNet.ApplicationCookie=" + token);

            return;
        }
        // GET: News_events1Data
        public ActionResult Index()
        {
            return View();
        }

        // GET: News_events1Data/Details/5
        public ActionResult Details(int id)
        {
            ShowNews_events ViewModel = new ShowNews_events();
            
            string url = "News_events1data/findNews_events/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            return View();
        }

        // GET: News_events1Data/Create


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            UpdateNews_events ViewModel = new UpdateNews_events();
            string url = "News_events1data/getNews_events";
            HttpResponseMessage response = client.GetAsync(url).Result;
            return View();
        }

        // POST: News_events1Data/Create
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
        }

        // GET: News_events1Data/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            UpdateNews_events ViewModel = new UpdateNews_events();
            string url = "News_events1data/findNews_events/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {

                News_eventsDto SelectedNews = response.Content.ReadAsAsync<News_eventsDto>().Result;
                ViewModel.News_events = SelectedNews;


                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: News_events1Data/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, News_events NewsInfo, HttpPostedFileBase NewsPic)
        {
            GetApplicationCookie();
            string url = "News_events1data/updateNews_events/" + id;
            Debug.WriteLine(jss.Serialize(NewsInfo));
            HttpContent content = new StringContent(jss.Serialize(NewsInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

                if (NewsPic != null)
                {
                    Debug.WriteLine("Calling Update Image method.");
                    url = "News_events1data/updatenewspic/" + id;

                    MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                    HttpContent imagecontent = new StreamContent(NewsPic.InputStream);
                    requestcontent.Add(imagecontent, "NewsPic", NewsPic.FileName);
                    response = client.PostAsync(url, requestcontent).Result;
                }

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: News_events1Data/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "News_events1data/findNews_events/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                
                News_eventsDto SelectedNews = response.Content.ReadAsAsync<News_eventsDto>().Result;
                return View(SelectedNews);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: News_events1Data/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();

            string url = "News_events1data/deleteNews_events/" + id;
            HttpContent content = new StringContent("");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
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
