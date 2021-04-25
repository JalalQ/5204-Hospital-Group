using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using team2Geraldton.Models;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace team2Geraldton.Controllers
{
    public class VolunteerOpportunityController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;

        static VolunteerOpportunityController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44374/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: VolunteerOpportunity/List
        public ActionResult List()
        {
            string url = "volunteeropportunitydata/getvolunteeropportunities";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<VolunteerOpportunityDto> SelectedVolunteerOpportunities = response.Content.ReadAsAsync<IEnumerable<VolunteerOpportunityDto>>().Result;
                return View(SelectedVolunteerOpportunities);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: VolunteerOpportunity/Details/5
        public ActionResult Details(int id)
        {

            string url = "volunteeropportunitydata/findvolunteeropportunity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            if (response.IsSuccessStatusCode)
            {

                //Put data into Injury data transfer object
                VolunteerOpportunityDto SelectedVolunteerOpportunity = response.Content.ReadAsAsync<VolunteerOpportunityDto>().Result;
                return View(SelectedVolunteerOpportunity);

            }
            else
            {
                return RedirectToAction("Error");
            }

        }


        // GET: VolunteerOpportunity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VolunteerOpportunity/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(VolunteerOpportunity VolunteerOpportunityInfo)
        {
            Debug.WriteLine(VolunteerOpportunityInfo.OpportunityName);
            string url = "volunteeropportunitydata/addvolunteeropportunity";
            Debug.WriteLine(jss.Serialize(VolunteerOpportunityInfo));
            HttpContent content = new StringContent(jss.Serialize(VolunteerOpportunityInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {

                int OpportunityId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = OpportunityId });
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: VolunteerOpportunity/Edit/5
        public ActionResult Edit(int id)
        {

            string url = "volunteeropportunitydata/findvolunteeropportunity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                //Put data into Injury data transfer object
                VolunteerOpportunityDto SelectedVolunteerOpportunity = response.Content.ReadAsAsync<VolunteerOpportunityDto>().Result;
                return View(SelectedVolunteerOpportunity);
            }
            else
            {
                return RedirectToAction("Error");

            }
        }


        // POST: VolunteerOpportunity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, VolunteerOpportunity VolunteerOpportunityInfo)
        {
            Debug.WriteLine(VolunteerOpportunityInfo.OpportunityName);
            string url = "volunteeropportunitydata/updatevolunteeropportunity/" + id;
            Debug.WriteLine(jss.Serialize(VolunteerOpportunityInfo));
            HttpContent content = new StringContent(jss.Serialize(VolunteerOpportunityInfo));
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


        // GET: VolunteerOpportunity/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "volunteeropportunitydata/findvolunteeropportunity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {

                VolunteerOpportunityDto SelectedVolunteerOpportunity = response.Content.ReadAsAsync<VolunteerOpportunityDto>().Result;
                return View(SelectedVolunteerOpportunity);
            }
            else
            {
                return RedirectToAction("Error");

            }
        }


        // POST: VolunteerOpportunity/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "volunteeropportunitydata/deletevolunteeropportunity/" + id;
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
