using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using News_Events_Payments.Models;

namespace News_Events_Payments.Controllers
{
    public class News_eventsDataController : ApiController
    {
        private DataContext db = new DataContext();
        /// <summary>
        /// Gets news and events in the database.
        /// </summary>
        /// <returns>a list of news and events including their id </returns>
        /// <example>

        // GET: api/News_eventsData/GetNews_events
        public IQueryable<News_events> GetNews_Events()
        {
            return db.News_Events;
        }

        // GET: api/News_eventsData/GetNews_Events/{id}
        [ResponseType(typeof(News_events))]
        public IHttpActionResult GetNews_events(int id)
        {
            News_events news_events = db.News_Events.Find(id);
            if (news_events == null)
            {
                return NotFound();
            }

            return Ok(news_events);
        }

        // PUT: api/News_eventsData/5
        /** [ResponseType(typeof(void))]
         public IHttpActionResult PutNews_events(int id, News_events news_events)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (id != news_events.News_events_id)
             {
                 return BadRequest();
             }

             db.Entry(news_events).State = EntityState.Modified;

             try
             {
                 db.SaveChanges();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!News_eventsExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return StatusCode(HttpStatusCode.NoContent);
         }*/


        /// <summary>
        /// Updates a news/events item in the database given information about the News_events.
        /// </summary>
        /// <param name="id">The news/events id</param>
        /// <param name="News_events_title">A News_events object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/News_eventsData/UpdateNews_events/5
        /// FORM DATA: News_events JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateNews_events(int id, [FromBody] News_events News_events)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != News_events.News_events_id)
            {
                return BadRequest();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!News_eventsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Adds a news/events item to the database.
        /// </summary>
        /// <param name="News_events">A News_events object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        // POST: api/News_eventsData/AddNews_events/5
        // FORM DATA: News_events JSON Object

        [ResponseType(typeof(News_events))]
        [HttpPost]
        public IHttpActionResult PostNews_events(News_events news_events)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.News_Events.Add(news_events);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news_events.News_events_id }, news_events);
        }
        /// <summary>
        /// Deletes a news/events item in the database
        /// </summary>
        /// <param name="id">The id of the item to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/News_eventsData/DeleteNews_eventsData/5
        /// </example>
        
        [HttpPost]
        public IHttpActionResult DeleteNews_events(int id)
        {
            News_events news_events = db.News_Events.Find(id);
            if (news_events == null)
            {
                return NotFound();
            }

            db.News_Events.Remove(news_events);
            db.SaveChanges();

            return Ok(news_events);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool News_eventsExists(int id)
        {
            return db.News_Events.Count(e => e.News_events_id == id) > 0;
        }
    }
}