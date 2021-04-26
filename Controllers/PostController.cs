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
    public class PostController : Controller
    {
        //Http Client is the proper way to connect to a webapi
        //https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-5.0

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static PostController()
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


        // GET: Post/List
        
        public ActionResult List()
        {
            ListPosts ViewModel = new ListPosts();
            ViewModel.isadmin = User.IsInRole("Admin");
            string url = "PostData/GetPosts";
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<PostDto> SelectedPosts = response.Content.ReadAsAsync<IEnumerable<PostDto>>().Result;
                ViewModel.posts = SelectedPosts;
                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        // GET: POst/Details/5
        public ActionResult Details(int id)
        {
            string url = "postdata/findpost/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("hello");
                //Put data into post data transfer object
                PostDto SelectedPost = response.Content.ReadAsAsync<PostDto>().Result;
                return View(SelectedPost);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }



        //GET: Post/Edit/2
        public ActionResult Edit(int id)
        {
            string url = "Postdata/findpost/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Post data transfer object
                PostDto SelectedPost = response.Content.ReadAsAsync<PostDto>().Result;
                return View(SelectedPost);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(int id, Post PostInfo)
        {
            Debug.WriteLine(PostInfo.Title);
            string url = "Postdata/updatepost/" + id;
            Debug.WriteLine(jss.Serialize(PostInfo));
            HttpContent content = new StringContent(jss.Serialize(PostInfo));
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



        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Post PostInfo)
        {
            Debug.WriteLine(PostInfo.Title);
            string url = "Postdata/addPost";
            Debug.WriteLine(jss.Serialize(PostInfo));
            HttpContent content = new StringContent(jss.Serialize(PostInfo));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                int postid = response.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Details", new { id = postid });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        
        
        
        // GET: Post/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Postdata/findpost/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            //Can catch the status code (200 OK, 301 REDIRECT), etc.
            //Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                //Put data into Customer data transfer object
                PostDto SelectedPost = response.Content.ReadAsAsync<PostDto>().Result;
                return View(SelectedPost);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Delete(int id)
        {
            string url = "postdata/deletepost/" + id;
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

