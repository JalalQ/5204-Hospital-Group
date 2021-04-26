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
    public class reviewDataController : ApiController
    {
        private team2GeraldtonDbContext db = new team2GeraldtonDbContext();


        /// <summary>
        /// For testing purpose only.
        /// </summary>
        /// <returns>Hello World String</returns>
        [Route("api/reviewData/helloworld")]
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }


        /*
        // GET: api/reviewData
        public IQueryable<review> Getreviews()
        {
            return db.reviews;
        }
        */


        /// <summary>
        /// Gets a list or Reviews in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Reviews</returns>
        /// <example>
        /// GET: api/ReviewData/GetReviews
        /// </example>
        [ResponseType(typeof(IEnumerable<reviewDto>))]
        public IHttpActionResult GetReviews()
        {
            List<review> reviews = db.reviews.Include(t => t.doctor).ToList();
            List<reviewDto> reviewDtos = new List<reviewDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Review in reviews)
            {
                reviewDto NewReview = new reviewDto
                {

                    reviewID = Review.reviewID,
                    knowledge = Review.knowledge,
                    professional = Review.professional,
                    friendly = Review.friendly,
                    doctorName = Review.doctor.fullName,

            };
                reviewDtos.Add(NewReview);
            }

            return Ok(reviewDtos);
        }

        // GET: api/reviewData/5
        [ResponseType(typeof(review))]
        public IHttpActionResult Getreview(int id)
        {
            review review = db.reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
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
        [ResponseType(typeof(reviewDto))]
        //[Route("api/reviewsData/FindReview/{id}")]
        public IHttpActionResult FindReview(int id)
        {
            //Find the data
            review Review = db.reviews.Find(id);
            //if not found, return 404 status code.
            if (Review == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            reviewDto reviewDto = new reviewDto
            {
                reviewID = Review.reviewID,
                knowledge = Review.knowledge,
                professional = Review.professional,
                friendly = Review.friendly,
                doctorID = Review.doctorID,
                doctorName = Review.doctor.fullName
            };


            //pass along data as 200 status code OK response
            return Ok(reviewDto);
        }



        /// <summary>
        /// Gets a list of reviews in the database alongside a status code (200 OK). 
        /// Skips the first {startindex} records and takes {perpage} records.
        /// </summary>
        /// <returns>A list of doctors</returns>
        /// <param name="StartIndex">The number of records to skip through</param>
        /// <param name="PerPage">The number of records for each page</param>
        /// <example>
        /// GET: api/doctorsData/getreviewspage/20/5
        /// Retrieves the first 5 players after skipping 20 (fifth page)
        /// 
        /// GET: api/reviewData/getreviewspage/15/3
        /// Retrieves the first 3 players after skipping 15 (sixth page)
        /// 
        /// </example>
        [ResponseType(typeof(IEnumerable<reviewDto>))]
        [Route("api/reviewData/getreviewspage/{StartIndex}/{PerPage}")]
        public IHttpActionResult GetReviewsPage(int StartIndex, int PerPage)
        {
            List<review> reviews = db.reviews.OrderBy(p => p.reviewID).Skip(StartIndex).Take(PerPage).ToList();
            List<reviewDto> reviewDtos = new List<reviewDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Review in reviews)
            {
                reviewDto NewReview = new reviewDto
                {

                    reviewID = Review.reviewID,
                    knowledge = Review.knowledge,
                    professional = Review.professional,
                    friendly = Review.friendly,
                    doctorName = Review.doctor.fullName

                };
                reviewDtos.Add(NewReview);
            }

            return Ok(reviewDtos);
        }





        // PUT: api/reviewData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putreview(int id, review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != review.reviewID)
            {
                return BadRequest();
            }

            db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!reviewExists(id))
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

        // POST: api/reviewData
        [ResponseType(typeof(review))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult addReview([FromBody] review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.reviews.Add(review);
            db.SaveChanges();

            return Ok(review.reviewID);

            //return CreatedAtRoute("DefaultApi", new { id = review.reviewID }, review);
        }


        /// <summary>
        /// Deletes a Review from the database
        /// </summary>
        /// <param name="id">The id of the Review to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST DELETE: api/reviewData/5
        /// </example>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ResponseType(typeof(review))]
        public IHttpActionResult Deletereview(int id)
        {
            review review = db.reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.reviews.Remove(review);
            db.SaveChanges();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool reviewExists(int id)
        {
            return db.reviews.Count(e => e.reviewID == id) > 0;
        }
    }
}