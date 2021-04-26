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
using System.Diagnostics;

namespace News_Events_Payments.Controllers
{
    public class Payments1DataController : ApiController
    {
        private DataContext db = new DataContext();
        /// <summary>
        /// Gets a list of payments in the database.
        /// </summary>
        /// <returns>a list of payments in the database including their id </returns>
        /// <example>

        // GET: api/Payments1/GetPayments
        public IQueryable<PaymentDto> GetPayments()
        {
            List<Payments> Payment = db.Payments.ToList();
            List<PaymentDto> PaymentDtos = new List<PaymentDto> { };

            foreach (var Payments in Payment)
            {
                PaymentDto NewPayments = new PaymentDto
                {
                    Payment_id = Payments.Payment_id,
                    Patient_firstname = Payments.Patient_firstname,
                    Patient_lastname = Payments.Patient_lastname,
                    Patient_email = Payments.Patient_email,
                    Payment_date = Payments.Payment_date,
                    Payment_amount = Payments.Payment_amount,
                    Bill_number = Payments.Bill_number,
                    Card_number = Payments.Card_number,
                    Card_csv = Payments.Card_csv

                    
                };
                PaymentDtos.Add(NewPayments);
            }

            return PaymentDtos;
        }
        /// <summary>
        /// Finds a payment based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a payment based on its id</returns>
        // GET: api/Payments1/FindPayments/5
        [ResponseType(typeof(PaymentDto))]
        [HttpGet]
        public IHttpActionResult FindPayments(int id)
        {
            Payments Payments = db.Payments.Find(id);
            if (Payments == null)
            {
                return NotFound();
            }

            PaymentDto PaymentDto = new PaymentDto
            {
                Payment_id = Payments.Payment_id,
                Patient_firstname = Payments.Patient_firstname,
                Patient_lastname = Payments.Patient_lastname,
                Patient_email = Payments.Patient_email,
                Payment_date = Payments.Payment_date,
                Payment_amount = Payments.Payment_amount,
                Bill_number = Payments.Bill_number,
                Card_number = Payments.Card_number,
                Card_csv = Payments.Card_csv
            };

            return Ok(PaymentDto);
        }
        /// <summary>
        /// Updates a payment based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Payments"></param>
        /// <returns>adds updated payment to the database based on its id</returns>
        // POST: api/Payments1/UpdatePayment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePayment(int id, [FromBody] Payments Payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Payments.Payment_id)
            {
                return BadRequest();
            }

            db.Entry(Payments).State = EntityState.Modified;

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
        }
        /// <summary>
        /// Adds a new payment to the database
        /// </summary>
        /// <param name="Payments"></param>
        /// <returns>If payment info is successfully filled out it will be added to the database</returns>
        // POST: api/Payments1/AddPayments
        [ResponseType(typeof(Payments))]
        [HttpPost]
        public IHttpActionResult AddPayments([FromBody] Payments Payments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(Payments);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Payments.Payment_id }, Payments);
        }

        
        /// <summary>
        /// Deletes a payment based on its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deletes payment from database</returns>
        // POST: api/Payments1/DeletePayments/5
        [HttpPost]
        public IHttpActionResult DeletePayments(int id)
        {
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payments);
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

        private bool PaymentsExists(int id)
        {
            return db.Payments.Count(e => e.Payment_id == id) > 0;
        }
    }
}