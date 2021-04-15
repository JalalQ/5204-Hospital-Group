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
    public class StaffDataController : ApiController
    {
        private team2GeraldtonDbContext db = new team2GeraldtonDbContext();

        /// <summary>
        /// Gets a list of all staff members in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of staff including their ID, firstname,lastname, email, contact, phone and department</returns>
        /// <example>
        /// GET: api/StaffData/GetStaffs
        /// </example>
        [ResponseType(typeof(IEnumerable<StaffDto>))]
        public IHttpActionResult GetStaffs()
        {
            List<Staff> Staffs = db.Staffs.ToList();
            List<StaffDto> StaffDtos = new List<StaffDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Staff in Staffs)
            {
                StaffDto NewStaff = new StaffDto
                {
                    StaffId = Staff.StaffId,
                    FirstName = Staff.FirstName,
                    LastName = Staff.LastName,
                    Email = Staff.Email,
                    Contact = Staff.Contact,
                    Position = Staff.Position
                };
                StaffDtos.Add(NewStaff);
            }
            return Ok(StaffDtos);
        }


        /// <summary>
        /// Finds a particular Staff in the database with a 200 status code. If the Staff is not found, return 404.
        /// </summary>
        /// <param name="id">The Staff id</param>
        /// <returns>Information about the Staff, including  ID, firstname,lastname, email, contact, phone and department</returns>
        // <example>
        // GET: api/StaffData/FindStaff/2
        // </example>
        [HttpGet]
        [ResponseType(typeof(StaffDto))]
        public IHttpActionResult FindStaff(int id)
        {
            //Find the data
            Staff Staff = db.Staffs.Find(id);
            //if not found, return 404 status code.
            if (Staff == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            StaffDto StaffDto = new StaffDto
            {
                StaffId = Staff.StaffId,
                FirstName = Staff.FirstName,
                LastName = Staff.LastName,
                Email = Staff.Email,
                Contact = Staff.Contact,
                Position = Staff.Position
            };
            //pass along data as 200 status code OK response
            return Ok(StaffDto);
        }



        /// <summary>
        /// Updates the information given in the database about the Staff
        /// </summary>
        /// <param name="id">The staff id</param>
        /// <param name="staff">A staff object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/StaffData/UpdateStaff/2
        /// FORM DATA: staff JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateStaff(int id, [FromBody] Staff Staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Staff.StaffId)
            {
                return BadRequest();
            }
            db.Entry(Staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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
        /// Add a new staff to the database.
        /// </summary>
        /// <param name="staff">A staff object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/StaffData/AddStaff
        ///  FORM DATA: Staff JSON Object
        /// </example>
        [ResponseType(typeof(Staff))]
        [HttpPost]
        public IHttpActionResult AddStaff([FromBody] Staff staff)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Staffs.Add(staff);
            db.SaveChanges();

            return Ok(staff.StaffId);
        }

        /// <summary>
        /// Deletes a staff member from the database
        /// </summary>
        /// <param name="id">The id of the Staff to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/StaffData/DeleteStaff/2
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteStaff(int id)
        {
            Staff Staff = db.Staffs.Find(id);
            if (Staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(Staff);
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

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.StaffId == id) > 0;
        }
    }
}