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
using GeraldtonHospital_v1.Models;
using System.Diagnostics;

namespace GeraldtonHospital_v1.Controllers
{
    public class DonorDataController : ApiController
    {
        //This variable is our database access point
        //the name is from Models/IdentityModel.cs and was changed to reflect the name of this application
        private GDbContext db = new GDbContext();

        //This code is mostly scaffolded from the base models and database context
        //New > WebAPIController with Entity Framework Read/Write Actions
        //Choose model "Donor"
        //Choose context "GDbContext"
        //Note: The base scaffolded code needs many improvements for a fully
        //functioning MVP.


        /// <summary>
        /// Gets a list or Donors in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Donors including their ID, name, and URL.</returns>
        /// <example>
        /// GET: api/DonorData/GetDonors
        /// </example>
        [ResponseType(typeof(IEnumerable<DonorDto>))]
        public IHttpActionResult GetDonors()
        {
            List<Donor> Donors = db.Donors.ToList();
            List<DonorDto> DonorDtos = new List<DonorDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Donor in Donors)
            {
                DonorDto NewDonor = new DonorDto
                {
                    DonorID = Donor.DonorID,
                    FirstName = Donor.FirstName,
                    LastName = Donor.LastName,
                    Email = Donor.Email,
                    Address= Donor.Address,
                    City= Donor.City,
                    Province= Donor.Province,
                    Country= Donor.Country,
                    PostalCode = Donor.PostalCode,
                    DonorMessage = Donor.DonorMessage
                };
                DonorDtos.Add(NewDonor);
            }

            return Ok(DonorDtos);
        }


        /// <summary>
        /// Gets a list of Donations in the database alongside a status code (200 OK).
        /// </summary>
        /// <param name="id">The input Donorid</param>
        /// <returns>A list of Donations associated with the Donor</returns>
        /// <example>
        /// GET: api/DonorData/GetDonationsForDonor
        /// </example>
        [ResponseType(typeof(IEnumerable<DonationDto>))]
        public IHttpActionResult GetDonationsForDonor(int id)
        {   //
            List<Donation> Donations = db.Donations.Where(p => p.DonationID == id) 
                .ToList();
            List<DonationDto> DonationDtos = new List<DonationDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Donation in Donations)
            {
                DonationDto NewDonation = new DonationDto
                {
                    DonationID = Donation.DonationID,
                    CardNum = Donation.CardNum,
                    Month = Donation.Month,
                    Year = Donation.Year,
                    Cvc = Donation.Cvc,
                    Amount = Donation.Amount
                };
                DonationDtos.Add(NewDonation);
            }

            return Ok(DonationDtos);
        }

        

        /// <summary>
        /// Finds a particular Donor in the database with a 200 status code. If the Donor is not found, return 404.
        /// </summary>
        /// <param name="id">The Donor id</param>
        /// <returns>Information about the Donor, including Donor id, bio, first and last name, and Donorid</returns>
        // <example>
        // GET: api/DonorData/FindDonor/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(DonorDto))]
        public IHttpActionResult FindDonor(int id)
        {
            //Find the data
            Donor Donor = db.Donors.Find(id);
            //if not found, return 404 status code.
            if (Donor == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            DonorDto DonorDto = new DonorDto
            {
                DonorID = Donor.DonorID,
                FirstName = Donor.FirstName,
                LastName = Donor.LastName,
                Email = Donor.Email,
                Address = Donor.Address,
                City = Donor.City,
                Province = Donor.Province,
                //Country= Donor.Country,
                PostalCode = Donor.PostalCode,
                DonorMessage = Donor.DonorMessage
            };


            //pass along data as 200 status code OK response
            return Ok(DonorDto);
        }

        /// <summary>
        /// Updates a Donor in the database given information about the Donor.
        /// </summary>
        /// <param name="id">The Donor id</param>
        /// <param name="Donor">A Donor object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/DonorData/UpdateDonor/5
        /// FORM DATA: Donor JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDonor(int id, [FromBody] Donor Donor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Donor.DonorID)
            {
                return BadRequest();
            }

            db.Entry(Donor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonorExists(id))
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
        /// Adds a Donor to the database.
        /// </summary>
        /// <param name="Donor">A Donor object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/DonorData/AddDonor
        ///  FORM DATA: Donor JSON Object
        /// </example>
        [ResponseType(typeof(Donor))]
        [HttpPost]
        public IHttpActionResult AddDonor([FromBody] Donor Donor)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donors.Add(Donor);
            db.SaveChanges();

            return Ok(Donor.DonorID);
        }

        /// <summary>
        /// Deletes a Donor in the database
        /// </summary>
        /// <param name="id">The id of the Donor to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/DonorData/DeleteDonor/5
        /// </example>
        [HttpPost]
        public IHttpActionResult DeleteDonor(int id)
        {
            Donor Donor = db.Donors.Find(id);
            if (Donor == null)
            {
                return NotFound();
            }

            db.Donors.Remove(Donor);
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

        /// <summary>
        /// Finds a Donor in the system. Internal use only.
        /// </summary>
        /// <param name="id">The Donor id</param>
        /// <returns>TRUE if the Donor exists, false otherwise.</returns>
        private bool DonorExists(int id)
        {
            return db.Donors.Count(e => e.DonorID == id) > 0;
        }
    }
}