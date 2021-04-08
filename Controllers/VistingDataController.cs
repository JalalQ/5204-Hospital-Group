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
using Simran_hospital.Models;
using System.Diagnostics;

namespace Simran_hospital.Controllers
{
    public class VistingDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// Gets a list or visitngs in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Visitings including their ID,name ,starttime, endtime and description.</returns>
        /// <example>
        /// GET: api/VisitingData/GetVisitings
        /// </example>
        [ResponseType(typeof(IEnumerable<VisitingDto>))]
        public IHttpActionResult GetVisitings()
        {
            List<Visting> Visitings = db.Vistings.ToList();
            List<VisitingDto> VisitingDtos = new List<VisitingDto> { };

            foreach (var Visiting in Visitings)
            {
                VisitingDto NewVisiting = new VisitingDto
                {
                    DepartmentID = Visiting.DepartmentID,
                    DepartmentName = Visiting.DepartmentName,
                    StartTime = Visiting.StartTime,
                    EndTime = Visiting.EndTime,
                    Description = Visiting.Description
                };
                VisitingDtos.Add(NewVisiting);
            }
            return Ok(VisitingDtos);
        }


        /// <summary>
        /// Finds a particular visiting in the database with a 200 status code. If the visiting is not found, return 404.
        /// </summary>
        /// <param name="id">The visiting id</param>
        /// <returns>Information about the visiting, including visiting id, ,starttime, endtime and description.</returns>
        // <example>
        // GET: api/VistingData/FindVisiting/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(VisitingDto))]
        public IHttpActionResult FindVisiting(int id)
        {
            //Find the data
            Visting Visiting = db.Vistings.Find(id);
            //if not found, return 404 status code.
            if (Visiting == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            VisitingDto VisitingDto = new VisitingDto
            {
                DepartmentID = Visiting.DepartmentID,
                DepartmentName = Visiting.DepartmentName,
                StartTime = Visiting.StartTime,
                EndTime = Visiting.EndTime,
                Description = Visiting.Description
            };


            //pass along data as 200 status code OK response
            return Ok(VisitingDto);
        }


        /// <summary>
        /// Updates a visitng in the database given information about the Injury.
        /// </summary>
        /// <param name="id">The department id</param>
        /// <param name="visiting">A visitng object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/VistingData/UpdateVisiting/5
        /// FORM DATA: Visiting JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVisiting(int id, [FromBody] Visting Visiting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Visiting.DepartmentID)
            {
                Debug.WriteLine("hello");
                return BadRequest();
            }

            db.Entry(Visiting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistingExists(id))
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

        // GET: api/VistingData
        public IQueryable<Visting> GetVistings()
        {
            return db.Vistings;
        }

        // GET: api/VistingData/5
        [ResponseType(typeof(Visting))]
        public IHttpActionResult GetVisting(int id)
        {
            Visting visting = db.Vistings.Find(id);
            if (visting == null)
            {
                return NotFound();
            }

            return Ok(visting);
        }

        // PUT: api/VistingData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVisting(int id, Visting visting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visting.DepartmentID)
            {
                return BadRequest();
            }

            db.Entry(visting).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VistingExists(id))
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

        // POST: api/VistingData
        [ResponseType(typeof(Visting))]
        public IHttpActionResult PostVisting(Visting visting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vistings.Add(visting);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = visting.DepartmentID }, visting);
        }

        // DELETE: api/VistingData/5
        [ResponseType(typeof(Visting))]
        public IHttpActionResult DeleteVisting(int id)
        {
            Visting visting = db.Vistings.Find(id);
            if (visting == null)
            {
                return NotFound();
            }

            db.Vistings.Remove(visting);
            db.SaveChanges();

            return Ok(visting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VistingExists(int id)
        {
            return db.Vistings.Count(e => e.DepartmentID == id) > 0;
        }
    }
}