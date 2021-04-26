using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using team2Geraldton.Models;
using team2Geraldton.Models.View_Models;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net.Http;

//using team2Geraldton.Models.ViewModels;
namespace team2Geraldton.Controllers
{
    public class appointmentsController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private object bookId;
        static readonly HttpClient client = new HttpClient();
        // GET: appointment
        public ActionResult Index()
        {
            return View();
        }
        static appointmentsController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
            };

            client = new HttpClient(handler);
            //change this to match your own local port number

            client.BaseAddress = new Uri("https://localhost:44346/api/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }
        //Admin can see appointment list
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            listAppointment viewModel = new listAppointment();
            // Grab all appointments
            string url= "appointmentsData/getappointment";
           
         
            // Send off an HTTP request
            // GET : /api/appointmentsdata/getAppointment
            // Retrieve response
            HttpResponseMessage response = client.GetAsync(url).Result;
            // If the response is a success, proceed
            if (response.IsSuccessStatusCode)
            {
            // Fetch the response content into IEnumerable<apoointmentDto>
            IEnumerable<appointmentDto> SelectedAppointments = response.Content.ReadAsAsync < IEnumerable<appointmentDto>>().Result;

                viewModel.appointments = SelectedAppointments;
                return View(viewModel);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
               return RedirectToAction("Error");
           }

        }

        private ActionResult ViewModel(IEnumerable<appointmentDto> selectedAppointments)
       {
            throw new NotImplementedException();
       }

        // GET: appointment/List
        // GET: appointment/Details/5
        //user that booked apointment and Admin can see details
        [Authorize(Roles = "Admin,registereduser")]
        public ActionResult Details(int id)
            {
              chooseDoctor ViewModel = new chooseDoctor();

               string url = "appointmentsData/Findappointment/" + id;
                HttpResponseMessage response = client.GetAsync(url).Result;

                //Can catch the status code (200 OK, 301 REDIRECT), etc.
                //Debug.WriteLine(response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                  appointmentDto Selectedapp = response.Content.ReadAsAsync<appointmentDto>().Result;
                ViewModel.appointment = Selectedapp;
                url = "appointmentsData/findDoctorForAppointment/" + id;
                response = client.GetAsync(url).Result;
                doctorDto SelectedDoctor = response.Content.ReadAsAsync<doctorDto>().Result;
                ViewModel.doctor=SelectedDoctor;

                return View(ViewModel);
            }
                else
                {
                    return RedirectToAction("Error");
                }
                return View();
            }


        // GET: appointments/Create
        [Authorize(Roles = "Registereduser")]
        public ActionResult Create()
        {
            //UpdateAppointment ViewModel = new UpdateAppointment();
            //get information about teams this player COULD play for.
            string url = "appointments/getappointment";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<appointmentDto> PotentialTeams = response.Content.ReadAsAsync<IEnumerable<appointmentDto>>().Result;
           // ViewModel.allteams = PotentialTeams;

            return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(appointment appointInfo)
        {
            //Debug.WriteLine(appointInfo.bookId);
            string url = "appointmentdata/Postappointment";
            //Debug.WriteLine(jss.Serialize(appointInfo));
            HttpContent content = new StringContent(jss.Serialize(appointInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("Details", new { id=bookId });

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

        // GET: appointments/Edit/1


        [Authorize(Roles = "Registereduser")]
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
                return View(Selectapp);
            }
            else
            {
                return RedirectToAction("Error");

            }
        }
        

        // POST: appointment/Edit/5
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

        // GET: appointments/Delete/2
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "appointmentsData/Findappointment/" + id;
           
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into player data transfer object
                appointmentDto Selectedapp= response.Content.ReadAsAsync<appointmentDto>().Result;
                return View(Selectedapp);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: appointment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "appointmentsData/Deleteappointment/" + id;
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

