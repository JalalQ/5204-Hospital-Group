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
    public class Payments1Controller : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;
        // GET: Payments1
        static Payments1Controller()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false,
                
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

        // GET: Payments/List?{PageNum}
        public ActionResult List(int PageNum = 0)
        {

            ListPayments ViewModel = new ListPayments();
            ViewModel.isadmin = User.IsInRole("Admin");


            string url = "Payments1Data/getpayments";
            // GET : /api/Payments1Data/getpayments
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PaymentDto> SelectedPayments = response.Content.ReadAsAsync<IEnumerable<PaymentDto>>().Result;

                int PaymentCount = SelectedPayments.Count();
                int PerPage = 8;
                int MaxPage = (int)Math.Ceiling((decimal)PaymentCount / PerPage) - 1;

                if (MaxPage < 0) MaxPage = 0;
                if (PageNum < 0) PageNum = 0;
                if (PageNum > MaxPage) PageNum = MaxPage;

                int StartIndex = PerPage * PageNum;

                ViewData["PageNum"] = PageNum;
                ViewData["PageSummary"] = " " + (PageNum + 1) + " of " + (MaxPage + 1) + " ";



                // GET api/payments1data/getpaymentsspage/{startindex}/{perpage}
                url = "payments1data/getpaymentspage/" + StartIndex + "/" + PerPage;
                response = client.GetAsync(url).Result;

                // Retrieve the response of the HTTP Request
                IEnumerable<PaymentDto> SelectedPaymentsPage = response.Content.ReadAsAsync<IEnumerable<PaymentDto>>().Result;

                ViewModel.payments = SelectedPaymentsPage;

                
                return View(ViewModel);

            }
            else
            {
                // If we reach here something went wrong with our list algorithm
                return RedirectToAction("Error");
            }


        }

        // GET: Payments1/Details/5
        public ActionResult Details(int id)
        {
            ShowPayments ViewModel = new ShowPayments();

            //Pass along to the view information about who is logged in
            ViewModel.isadmin = User.IsInRole("Admin");



            string url = "payments1data/findpayment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                
                PaymentDto SelectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;
                ViewModel.Payment = SelectedPayment;
                url = "payments1data/findusersforpayment/" + id;
                response = client.GetAsync(url).Result;
                UsersDto SelectedUser = response.Content.ReadAsAsync<UsersDto>().Result;
                ViewModel.user = SelectedUser;


                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [Authorize(Roles = "Admin, Patient")]
        // GET: Payments1/Create
        public ActionResult Create()
        {
            UpdatePayment ViewModel = new UpdatePayment();
            
            string url = "userdata/getusers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<UsersDto> PotentialUsers = response.Content.ReadAsAsync<IEnumerable<UsersDto>>().Result;
            ViewModel.allusers = PotentialUsers;

            return View(ViewModel);
        }

        // POST: Payments1/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin, Patient")]
        public ActionResult Create(Payments PaymentsInfo)
        {
            GetApplicationCookie();

            
            string url = "payments1data/addpayment";
            HttpContent content = new StringContent(jss.Serialize(PaymentsInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int paymentsid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = paymentsid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Payments1/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            UpdatePayments ViewModel = new UpdatePayments();

            string url = "payments1data/findpayments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                
                PaymentDto SelectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;
                ViewModel.payment = SelectedPayment;

                
                url = "payments1data/getusers";
                response = client.GetAsync(url).Result;
                IEnumerable<UsersDto> PotentialUsers = response.Content.ReadAsAsync<IEnumerable<UsersDto>>().Result;
                ViewModel.allusers = PotentialUsers;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Payments1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Payments PaymentsInfo)
        {
            GetApplicationCookie();

           
            string url = "payments1data/updatepayment/" + id;
            Debug.WriteLine(jss.Serialize(PaymentsInfo));
            HttpContent content = new StringContent(jss.Serialize(PaymentsInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {

                
                if (PaymentsPic != null)
                {
                    Debug.WriteLine("Calling Update Image method.");
                    
                    url = "payments1data/updatepaymentspic/" + id;
                    

                    MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                    HttpContent imagecontent = new StreamContent(PaymentsPic.InputStream);
                    requestcontent.Add(imagecontent, "PaymentsPic", PaymentsPic.FileName);
                    response = client.PostAsync(url, requestcontent).Result;
                }

                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Payments1/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "paymentsdata/findpayments/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            
            if (response.IsSuccessStatusCode)
            {
                
                PaymentDto SelectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;
                return View(SelectedPayment);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Payments1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            GetApplicationCookie();

            string url = "payments1data/deletepayments/" + id;
            
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
