using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using hospitalPrj.Models;

namespace hospitalPrj.Controllers
{
    public class clientController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static readonly HttpClient client = new HttpClient();
        // GET: appointment
        public ActionResult Index()
        {
            return View();
        }
        static clientController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };

            client = new HttpClient(handler);
            //change this to match your own local port number
            client.BaseAddress = new Uri("https://localhost:44381/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult List()
        {

            // Grab all appointment
            string url = "clientsData / getClient";
            // Send off an HTTP request
            // GET : /api/clientdata/getClient
            // Retrieve response
            HttpResponseMessage response = client.GetAsync(url).Result;
            // If the response is a success, proceed
            if (response.IsSuccessStatusCode)
            {
                // Fetch the response content into IEnumerable<apoointmentDto>
                IEnumerable<clientDto> selectClient = response.Content.ReadAsAsync<IEnumerable<clientDto>>().Result;

                return View(selectClient);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
                return RedirectToAction("Error");
            }


        }
        

        // GET: client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: client/Create
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

        // GET: client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: client/Edit/5
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

        // GET: client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: client/Delete/5
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
