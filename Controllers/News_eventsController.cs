using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News_Events_Payments.Models;

namespace News_Events_Payments.Controllers
{
    public class News_eventsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: News_events
        public ActionResult Index()
        {
            return View(db.News_Events.ToList());
        }

        // GET: News_events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_events news_events = db.News_Events.Find(id);
            if (news_events == null)
            {
                return HttpNotFound();
            }
            return View(news_events);
        }

        // GET: News_events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News_events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "News_events_id,News_events_title,News_events_content,Date_published")] News_events news_events)
        {
            if (ModelState.IsValid)
            {
                db.News_Events.Add(news_events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news_events);
        }

        // GET: News_events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_events news_events = db.News_Events.Find(id);
            if (news_events == null)
            {
                return HttpNotFound();
            }
            return View(news_events);
        }

        // POST: News_events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "News_events_id,News_events_title,News_events_content,Date_published")] News_events news_events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news_events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news_events);
        }

        // GET: News_events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_events news_events = db.News_Events.Find(id);
            if (news_events == null)
            {
                return HttpNotFound();
            }
            return View(news_events);
        }

        // POST: News_events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News_events news_events = db.News_Events.Find(id);
            db.News_Events.Remove(news_events);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
