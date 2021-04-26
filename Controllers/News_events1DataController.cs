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
    public class News_events1DataController : ApiController
    {
        private DataContext db = new DataContext();
        /// <summary>
        /// Gets a list of news/events in the database.
        /// </summary>
        /// <returns>a list of news/events in the database including their id </returns>
        /// <example>
        // GET: api/News_events1/GetNews
        public IEnumerable<News_eventsDto> GetNews()
        {
            List<News_events> News = db.News_events.ToList();
            List<News_eventsDto> News_eventsDtos = new List<News_eventsDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var News_events in News)
            {
                News_eventsDto NewNews_events = new News_eventsDto
                {

                    News_events_id = News_events.News_events_id,
                    News_events_title = News_events.News_events_title,
                    News_events_content = News_events.News_events_content,
                    Date_published = News_events.Date_published
                };
                News_eventsDtos.Add(NewNews_events);
            }

            return News_eventsDtos;
        }

        /// <summary>
        /// Finds a news/event item based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a news/event item based on its id</returns>
        // GET: api/News_events1/FindNews_events/5
        [ResponseType(typeof(News_events))]
        [HttpGet]
        public IHttpActionResult FindNews_events(int News_events_id)
        {
            //Find the data
            News_events News_events = db.News_events.Find(News_events_id);
            //if not found, return 404 status code.
            if (News_events == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            News_eventsDto News_eventsDto = new News_eventsDto
            {
                News_events_id = News_events.News_events_id,
                News_events_title = News_events.News_events_title,
                News_events_content = News_events.News_events_content,
                Date_published = News_events.Date_published

            };


            //pass along data as 200 status code OK response
            return Ok(News_eventsDto);
        }
         /// <summary>
        /// Updates a news/event item based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="News_events"></param>
        /// <returns>adds updated news/event item to the database based on its id</returns>
        // POST: api/News_events1/UpdateNews_events/5
        [ResponseType(typeof(void))]
        [HttpPost]

        public IHttpActionResult UpdateNews_events(int id, [FromBody] News_events news_events)
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
        }
        /// <summary>
        /// Adds a news/event item to the database
        /// </summary>
        /// <param name="News_events"></param>
        /// <returns>If news/event info is successfully filled out it will be added to the database</returns>
        // POST: api/News_events1/AddNews_events

        [ResponseType(typeof(News_events))]
        [HttpPost]
        public IHttpActionResult AddNews_events([FromBody] News_events news_events)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.News_events.Add(news_events);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news_events.News_events_id }, news_events);
        }
        
        /// <summary>
        /// Deletes a news/event item based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes news/event item from database</returns>
        // POST: api/News_events1/DeleteNews_events/5

        [HttpPost]

        public IHttpActionResult DeleteNews_events(int id)
        {
            News_events news_events = db.News_events.Find(id);
            if (news_events == null)
            {
                return NotFound();
            }

            db.News_events.Remove(news_events);
            db.SaveChanges();

            return Ok();
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
            return db.News_events.Count(e => e.News_events_id == id) > 0;
        }
    }
}
