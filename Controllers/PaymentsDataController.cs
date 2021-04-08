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
    public class PaymentsDataController : ApiController
    {
        private DataContext db = new DataContext();
        /// <summary>
        /// Gets payments in the database.
        /// </summary>
        /// <returns>a list of payments including their id </returns>
        /// <example>

        // GET: api/PaymentsData/GetPaymentsData

        public IQueryable<Payments> GetPayments()
        {
            return db.Payments;
        }

        // GET: api/PaymentsData/5
        [ResponseType(typeof(Payments))]
        public IHttpActionResult GetPayments(int id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        /* // PUT: api/PaymentsData/5
         [ResponseType(typeof(void))]
         public IHttpActionResult PutPayments(int id, Payments payments)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (id != payments.Payment_id)
             {
                 return BadRequest();
             }

             db.Entry(payments).State = EntityState.Modified;

             try
             {
                 db.SaveChanges();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!PaymentsExists(id))
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
        /// Updates a payment in the database given information about the News_events.
        /// </summary>
        /// <param name="id">The payment id</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/PaymentsData/UpdatePayments/5
        /// FORM DATA: Payments JSON Object
        /// </example>

        // POST: api/PaymentsData
        [ResponseType(typeof(Payments))]
        [HttpPost]
        public IHttpActionResult UpdatePayments(Payments payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payments.Payment_id }, payments);
        }

        /// <summary>
        /// Adds a payment to the database.
        /// </summary>
        /// <param name="Payments">A Payments object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        // POST: api/PaymentsData/AddPayments/5
        // FORM DATA: Payments JSON Object

        [ResponseType(typeof(Payments))]
        [HttpPost]
        public IHttpActionResult PostPayments(Payments payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payments.Payment_id }, payments);
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


        // DELETE: api/PaymentsData/5
        [ResponseType(typeof(Payments))]
        public IHttpActionResult DeletePayments(int id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payments);
            db.SaveChanges();

            return Ok(payments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentsExists(int id)
        {
            return db.Payments.Count(e => e.Payment_id == id) > 0;
        }
    }
}