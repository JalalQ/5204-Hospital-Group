using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hospitalPrj.Models;
using System.Diagnostics;

namespace hospitalPrj.Controllers
{
    public class appointmentDataController : ApiController
    {
        private HospitalDbContext db = new HospitalDbContext();

        /// GET : api/appointmentData/getAppointment
        [ResponseType(typeof(IEnumerable<appointmentDto>))]
        [Route("api/appointmentData/getAppointment")]
        public IHttpActionResult getAppointment()
        {

            List<appointment> Appointments = db.Appointments.ToList();
            List<appointmentDto> appDtos = new List<appointmentDto> { };
            //Information to expose ApI
            foreach (var ap in Appointments)
            {
                appointmentDto NewAppointment = new appointmentDto
                {
                    bookId = ap.bookId,
                    bookDate = ap.bookDate,
                    bookReason = ap.bookReason,
                    drName = ap.drName,
                    department=ap.department,
                    clientId=ap.clientId,

                };
                appDtos.Add(NewAppointment);
            }
            return Ok(appDtos);
        }

        // GET: api/appointmentData/5
        [HttpGet]
        [ResponseType(typeof(appointment))]
        public IHttpActionResult Findappointment(int id)
        {
            appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            appointmentDto NewApp = new appointmentDto
            {
                bookId = appointment.bookId,
                bookDate = appointment.bookDate,
                bookReason = appointment.bookReason,
                drName = appointment.drName,
                department = appointment.department,
                clientId = appointment.clientId,

            };


            return Ok(NewApp);
        }

        // PUT: api/appointmentData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Addappointment(int id, appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.bookId)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appointmentExists(id))
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

        // POST: api/appointmentData
        [ResponseType(typeof(appointment))]
        public IHttpActionResult Postappointment([FromBody] appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return Ok(appointment.bookId);
        }

        // DELETE: api/appointmentData/5
        [ResponseType(typeof(appointment))]
        public IHttpActionResult Deleteappointment(int id)
        {
            appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
            db.SaveChanges();

            return Ok(appointment);
        }
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id, [FromBody] appointment app)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != app.bookId)
            {
                return BadRequest();
            }


            db.Entry(app).State = EntityState.Modified;
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appointmentExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appointmentExists(int id)
        {
            return db.Appointments.Count(e => e.bookId == id) > 0;
        }
    }
}