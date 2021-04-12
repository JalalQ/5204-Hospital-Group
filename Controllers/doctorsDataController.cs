using System;
using System.IO;
using System.Web;
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
using System.Diagnostics;

namespace team2Geraldton.Controllers
{
    public class doctorsDataController : ApiController
    {
        private team2GeraldtonDbContext db = new team2GeraldtonDbContext();

        /// <summary>
        /// For testing purpose only.
        /// </summary>
        /// <returns>Hello World String</returns>
        [Route("api/doctorsData/helloworld")]
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        // GET: api/doctorsData/Getdoctors
        [Route("api/doctorsData/Getdoctors")]
        [HttpGet]
        public IQueryable<doctor> Getdoctors()
        {
            return db.doctors;
        }

        // GET: api/doctorsData/5
        [Route("api/doctorsData/Getdoctor/{id}")]
        [HttpGet]
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

        /// <summary>
        /// Gets a list or doctors in the database alongside a status code (200 OK). 
        /// Skips the first {startindex} records and takes {perpage} records.
        /// </summary>
        /// <returns>A list of doctors</returns>
        /// <param name="StartIndex">The number of records to skip through</param>
        /// <param name="PerPage">The number of records for each page</param>
        /// <example>
        /// GET: api/doctorsData/getdoctorspage/20/5
        /// Retrieves the first 5 players after skipping 20 (fifth page)
        /// 
        /// GET: api/doctorsData/getdoctorspage/15/3
        /// Retrieves the first 3 players after skipping 15 (sixth page)
        /// 
        /// </example>
        [ResponseType(typeof(IEnumerable<doctorDto>))]
        [Route("api/doctorsData/getdoctorspage/{StartIndex}/{PerPage}")]
        public IHttpActionResult GetDoctorsPage(int StartIndex, int PerPage)
        {
            List<doctor> doctors = db.doctors.OrderBy(p => p.doctorID).Skip(StartIndex).Take(PerPage).ToList();
            List<doctorDto> doctorDtos = new List<doctorDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Doctor in doctors)
            {
                doctorDto NewDoctor = new doctorDto
                {
                    doctorID = Doctor.doctorID,
                    fullName = Doctor.fullName,
                    cpsoReg = Doctor.cpsoReg,
                    email = Doctor.email,
                    education = Doctor.education,
                    expertise = Doctor.expertise,
                    biography = Doctor.biography

                };
                doctorDtos.Add(NewDoctor);
            }

            return Ok(doctorDtos);
        }

        /// <summary>
        /// Finds a particular doctor in the database with a 200 status code. If the player is not found, return 404.
        /// </summary>
        /// <param name="id">The player id</param>
        /// <returns>Information about the doctor, including player id, ... </returns>
        // <example>
        // GET: api/doctorData/FindDoctor/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(doctorDto))]
        [Route("api/doctorsData/FindDoctor/{id}")]
        public IHttpActionResult FindDoctor(int id)
        {
            //Find the data
            doctor Doctor = db.doctors.Find(id);
            //if not found, return 404 status code.
            if (Doctor == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            doctorDto doctorDto = new doctorDto
            {

                doctorID = Doctor.doctorID,
                fullName = Doctor.fullName,
                cpsoReg = Doctor.cpsoReg,
                email = Doctor.email,
                education = Doctor.education,
                expertise = Doctor.expertise,
                biography = Doctor.biography
            };


            //pass along data as 200 status code OK response
            return Ok(doctorDto);
        }


        /// <summary>
        /// Adds a doctor to the database.
        /// </summary>
        /// <param name="player">A doctor object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/doctorData/AddDoctor
        ///  FORM DATA: Doctor JSON Object
        /// </example>
        [ResponseType(typeof(doctor))]
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Route("api/doctorsData/AddDoctor")]
        public IHttpActionResult AddDoctor([FromBody] doctor doctor)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.doctors.Add(doctor);
            db.SaveChanges();

            return Ok(doctor.doctorID);
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