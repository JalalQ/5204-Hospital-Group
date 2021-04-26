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
using team2Geraldton.Models;

namespace team2Geraldton.Controllers
{
    public class doctorsDataController : ApiController
    {
        private team2GeraldtonDbContext db = new team2GeraldtonDbContext();

        // GET: api/doctorsData
        [ResponseType(typeof(IEnumerable<doctorDto>))]
        public IHttpActionResult Getdoctor()
        {
            List<doctor> doctors = db.doctors.Include(t=>t.Appointments).ToList();
            List<doctorDto> doctorDtos = new List<doctorDto> { };
            //Here you can choose which information is exposed to the API
            foreach (var d in doctors)
            {
                doctorDto Newdoctor = new doctorDto
                {
                    doctorID = d.doctorID,
                    fullName = d.fullName,
                    expertise = d.expertise,
                     numBook= d.Appointments.Count(),
                };
                doctorDtos.Add(Newdoctor);
            }

            return Ok(doctorDtos);
        }

        // GET: api/doctorsData/5
        [ResponseType(typeof(doctor))]
        public IHttpActionResult Getdoctor(int id)
        {
            doctor doctor = db.doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // PUT: api/doctorsData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdoctor(int id, doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.doctorID)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!doctorExists(id))
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

        // POST: api/doctorsData
        [ResponseType(typeof(doctor))]
        public IHttpActionResult Postdoctor(doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.doctors.Add(doctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctor.doctorID }, doctor);
        }

        // DELETE: api/doctorsData/5
        [ResponseType(typeof(doctor))]
        public IHttpActionResult Deletedoctor(int id)
        {
            doctor doctor = db.doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.doctors.Remove(doctor);
            db.SaveChanges();

            return Ok(doctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool doctorExists(int id)
        {
            return db.doctors.Count(e => e.doctorID == id) > 0;
        }
    }
}