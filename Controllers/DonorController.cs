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
            //======add your localhost port number
            client.BaseAddress = new Uri("https://localhost:44366/api/");
            //MediaTypeQualityHeaderValue provides support for the media type and quality in a Content-Type header : json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        // GET: Donor/List
        public ActionResult List()
        {
            string url = "donordata/getdonors";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<DonorDto> SelectedDonors = response.Content.ReadAsAsync<IEnumerable<DonorDto>>().Result;
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
            string url = "donordata/finddoor" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Debug.WriteLine(response.StatusCode); 
            if (response.IsSuccessStatusCode)
            {
                //Put data into Donor Data transfer object - see Donor Model for more details
                DonorDto SelectedDonor = response.Content.ReadAsAsync<DonorDto>().Result;
                ViewModel.donor = SelectedDonor; 


            }
            
            return View();
        }

        // GET: Donor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donor/Create
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

        // GET: Donor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Donor/Edit/5
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

        // GET: Donor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Donor/Delete/5
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
