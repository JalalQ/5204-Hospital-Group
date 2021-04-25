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
using GeraldtonHospV7.Models;
//using GeraldtonHospV7.Models.ViewModels;
using System.Diagnostics;

namespace GeraldtonHospV7.Controllers
{
    public class DonationDataController : ApiController
    {
        private GeraldtonHospV7DbContext db = new GeraldtonHospV7DbContext();

        //This code is mostly scaffolded from the base models and database context
        //New > WebAPIController with Entity Framework Read/Write Actions
        //Choose model "Donation"
        //Choose context "Varsity Data Context"
        //Note: The base scaffolded code needs many improvements for a fully
        //functioning MVP.


        /// <summary>
        /// Gets a list or Donations in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Donations including their ID, bio, first name, last name, and Donorid.</returns>
        /// <example>
        /// GET : api/Donationdata/getDonations
        /// </example>

        [ResponseType(typeof(IEnumerable<DonationDto>))]
        [Route("api/donationdata/getdonations")]
        public IHttpActionResult GetDonations()
        {
            List<Donation> Donations = db.Donations.ToList();
            List<DonationDto> DonationDtos = new List<DonationDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Donation in Donations)
            {
                DonationDto NewDonation = new DonationDto
                {
                    DonationID = Donation.DonationID,
                    Amount = Donation.Amount,
                    //DonorID = Donation.DonorID
                };
                DonationDtos.Add(NewDonation);
            }

            return Ok(DonationDtos);
        }

        /// <summary>
        /// Gets a list or Donations in the database alongside a status code (200 OK). Skips the first {startindex} records and takes {perpage} records.
        /// </summary>
        /// <returns>A list of Donations including their ID, bio, first name, last name, and Donorid.</returns>
        /// <param name="StartIndex">The number of records to skip through</param>
        /// <param name="PerPage">The number of records for each page</param>
        /// <example>
        /// GET: api/DonationData/GetDonationsPage/20/5
        /// Retrieves the first 5 Donations after skipping 20 (fifth page)
        /// 
        /// GET: api/DonationData/GetDonationsPage/15/3
        /// Retrieves the first 3 Donations after skipping 15 (sixth page)
        /// 
        /// </example>
        //[ResponseType(typeof(IEnumerable<DonationDto>))]
        //[Route("api/donationdata/getdonationspage/{StartIndex}/{PerPage}")]
        //public IHttpActionResult GetDonationsPage(int StartIndex, int PerPage)
        //{
        //    List<Donation> Donations = db.Donations.ToList();
        //    List<DonationDto> DonationDtos = new List<DonationDto> { };

        //    //Here you can choose which information is exposed to the API
        //    foreach (var Donation in Donations)
        //    {
        //        DonationDto NewDonation = new DonationDto
        //        {
        //            DonationID = Donation.DonationID,
        //            Amount = Donation.Amount,
        //            //DonorID = Donation.DonorID
        //        };
        //        DonationDtos.Add(NewDonation);
        //    }

        //    return Ok(DonationDtos);
        //}

        /// <summary>
        /// Finds a particular Donation in the database with a 200 status code. If the Donation is not found, return 404.
        /// </summary>
        /// <param name="id">The Donation id</param>
        /// <returns>Information about the Donation, including Donation id, bio, first and last name, and Donorid</returns>
        /// <example>
        ///GET: api/DonationData/FindDonation/5
        ///</example>
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IHttpActionResult FindDonation(int id)
        {
            //Find the data
            Donation Donation = db.Donations.Find(id);
            //if not found, return 404 status code.
            if (Donation == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format' that will be displayed for LIST view
            DonationDto DonationDto = new DonationDto
            {
                DonationID = Donation.DonationID,
                Amount = Donation.Amount,
                //DonorID = Donation.DonorID
            };


            //pass along data as 200 status code OK response
            return Ok(DonationDto);
        }

        /// <summary>
        /// Finds a particular Donor in the database given a Donation id with a 200 status code. If the Donor is not found, return 404.
        /// </summary>
        /// <param name="id">The Donation id</param>
        /// <returns>Information about the Donor, including Donor id, bio, first and last name, and Donorid</returns>
        // <example>
        // GET: api/DonorData/FindDonorForDonation/5
        // </example>
        [HttpGet]
        [ResponseType(typeof(DonationDto))]
        public IHttpActionResult FindDonorDonation(int id)
        {
            //Finds the first Donor which has any Donations
            //that match the input Donationid
            Donor Donor = db.Donors
                .Where(t => t.Donations.Any(p => p.DonationID == id))
                .FirstOrDefault();
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
                LastName = Donor.LastName
            };


            //pass along data as 200 status code OK response
            return Ok(DonorDto);
        }

        /// <summary>
        /// Updates a Donation in the database given information about the Donation.
        /// </summary>
        /// <param name="id">The Donation id</param>
        /// <param name="Donation">A Donation object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/DonationData/UpdateDonation/5
        /// FORM DATA: Donation JSON Object
        /// </example>

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateDonation(int id, [FromBody] Donation Donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Donation.DonationID)
            {
                return BadRequest();
            }


            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a Donation to the database.
        /// </summary>
        /// <param name="Donation">A Donation object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/DonationData/AddDonation
        ///  FORM DATA: Donation JSON Object
        /// </example>
        [HttpPost]
        [ResponseType(typeof(Donation))]
        public IHttpActionResult AddDonation([FromBody] Donation Donation)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Donations.Add(Donation);
            db.SaveChanges();

            return Ok(Donation.DonationID);
        }

        /// <summary>
        /// Deletes a Donation in the database
        /// </summary>
        /// <param name="id">The id of the Donation to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/DonationData/DeleteDonation/5
        /// </example>
        /// 

        [HttpPost]
        public IHttpActionResult DeleteDonation(int id)
        {
            Donation Donation = db.Donations.Find(id);
            if (Donation == null)
            {
                return NotFound();
            }
            

            db.Donations.Remove(Donation);
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
        /// Finds a Donation in the system. Internal use only.
        /// </summary>
        /// <param name="id">The Donation id</param>
        /// <returns>TRUE if the Donation exists, false otherwise.</returns>

        private bool DonationExists(int id)
        {
            return db.Donations.Count(e => e.DonationID == id) > 0;
        }
    }
}


// after setting up a WEBAPI controller: 
//****NEXT: go to App_Start folder > webapiconfig.cs file and add the action attribute to the route template
// routeTemplate: "api/{controller}/{action}/{id}",