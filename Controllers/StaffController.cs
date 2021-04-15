using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using team2Geraldton.Models;
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
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44346/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);
        }


        // GET: Staff/List
        public ActionResult List()
        {
            string url = "StaffData/GetStaffs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<StaffDto> SelectedStaffs = response.Content.ReadAsAsync<IEnumerable<StaffDto>>().Result;
                return View(SelectedStaffs);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            string url = "Staffdata/findStaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("hello");
                //Put data into staff data transfer object
                StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
                return View(SelectedStaff);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        //GET: Staff/Edit/2
        public ActionResult Edit(int id)
        {
            string url = "Staffdata/findStaff/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Staff data transfer object
                StaffDto SelectedStaff = response.Content.ReadAsAsync<StaffDto>().Result;
                return View(SelectedStaff);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST:Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Staff StaffInfo)
        {
            Debug.WriteLine(StaffInfo.FirstName);
            string url = "Staffdata/addStaff";
            Debug.WriteLine(jss.Serialize(StaffInfo));
            HttpContent content = new StringContent(jss.Serialize(StaffInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int Staffid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = Staffid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        // GET: Staff/Delete/5
        [HttpGet]
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
        public ActionResult Delete(int id)
        {
            string url = "Staffdata/deleteStaff/" + id;
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


