using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using hospitalPrj.Models;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace hospitalPrj.Controllers
{
    public class appointmentController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static readonly HttpClient client = new HttpClient();
        // GET: appointment
        public ActionResult Index()
        {
            return View();
        }
        static appointmentController()
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
            string url = "appointmentData / getAppointment";
            // Send off an HTTP request
            // GET : /api/appointmentdata/getAppointment
            // Retrieve response
            HttpResponseMessage response = client.GetAsync(url).Result;
            // If the response is a success, proceed
            if (response.IsSuccessStatusCode)
            {
                // Fetch the response content into IEnumerable<apoointmentDto>
                IEnumerable<appointmentDto> SelectedAppointments= response.Content.ReadAsAsync<IEnumerable<appointmentDto>>().Result;

                //ViewModel.Appointment = SelectedAppointments;
                return View(@SelectedAppointments);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
                return RedirectToAction("Error");
            }


        }

        // GET: appointment/Details/5
        // GET: appointment/List

        public ActionResult Details(int id)
        {
            string url = "appointmentData/Findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("hello");
                
                appointmentDto Selectedappointment= response.Content.ReadAsAsync<appointmentDto>().Result;
                return View(Selectedappointment);
            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }

        // GET: appointment/Create
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(appointment appointInfo)
        {
            Debug.WriteLine(appointInfo.bookId);
            string url ="appointmentData/addappointment";
            Debug.WriteLine(jss.Serialize(appointInfo));
            HttpContent content = new StringContent(jss.Serialize(appointInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int bookId = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = bookId });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: appointment/Create
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

        // GET: appointment/Edit/1
        public ActionResult Edit(int id)
        {

            string url = "appointmentdata/Findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                appointmentDto Selectapp = response.Content.ReadAsAsync<appointmentDto>().Result;

                url = "clentdata/getClient";
                response = client.GetAsync(url).Result;
                IEnumerable<clientDto> Pclient = response.Content.ReadAsAsync<IEnumerable<clientDto>>().Result;


                return View(Selectapp);
            }
            else
            {
                return RedirectToAction("Error");

            }
        }
        // GET: appointment/Delete/2
        [HttpGet]

        public ActionResult DeleteConfirm(int id)
        {
            string url = "appointmentData/Findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into player data transfer object
                appointmentDto SelectedPlayer = response.Content.ReadAsAsync<appointmentDto>().Result;
                return View(SelectedPlayer);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: appointment/Delete/5
        [ HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "appointmentData/Deleteappointment/" + id;
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
