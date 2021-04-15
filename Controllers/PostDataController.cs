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
    public class PostDataController : ApiController
    {
        private team2GeraldtonDbContext db = new team2GeraldtonDbContext();

        /// <summary>
        /// Gets a list or Posts in the database alongside a status code (200 OK).
        /// </summary>
        /// <returns>A list of Posts including their ID, title, type, and description.</returns>
        /// <example>
        /// GET: api/PostData/GetPosts
        /// </example>
        [ResponseType(typeof(IEnumerable<PostDto>))]
        public IHttpActionResult GetPosts()
        {
            List<Post> Posts = db.Posts.ToList();
            List<PostDto> PostDtos = new List<PostDto> { };

            //Here you can choose which information is exposed to the API
            foreach (var Post in Posts)
            {
                PostDto NewPost = new PostDto
                {
                    PostId = Post.PostId,
                    Title = Post.Title,
                    Type = Post.Type,
                    Description = Post.Description
                };
                PostDtos.Add(NewPost);
            }
            return Ok(PostDtos);
        }


        /// <summary>
        /// Finds a particular Post in the database with a 200 status code. If the post is not found, return 404.
        /// </summary>
        /// <param name="id">The Post id</param>
        /// <returns>Information about the Post, including id, title, type and description</returns>
        // <example>
        // GET: api/PostData/FindPost/2
        // </example>
        [HttpGet]
        [ResponseType(typeof(PostDto))]
        public IHttpActionResult FindPost(int id)
        {
            //Find the data
            Post Post = db.Posts.Find(id);
            //if not found, return 404 status code.
            if (Post == null)
            {
                return NotFound();
            }

            //put into a 'friendly object format'
            PostDto PostDto = new PostDto
            {
                PostId = Post.PostId,
                Title = Post.Title,
                Type = Post.Type,
                Description = Post.Description
            };
            //pass along data as 200 status code OK response
            return Ok(PostDto);
        }



        /// <summary>
        /// Updates the information given in the database about the Post
        /// </summary>
        /// <param name="id">The post id</param>
        /// <param name="post">A post object. Received as POST data.</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/PostData/UpdatePost/2
        /// FORM DATA: post JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePost(int id, [FromBody] Post Post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Post.PostId)
            {
                return BadRequest();
            }

            db.Entry(Post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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
        /// Add a new post to the database.
        /// </summary>
        /// <param name="post">A post object. Sent as POST form data.</param>
        /// <returns>status code 200 if successful. 400 if unsuccessful</returns>
        /// <example>
        /// POST: api/PostData/AddPost
        ///  FORM DATA: Post JSON Object
        /// </example>
        [ResponseType(typeof(Post))]
        [HttpPost]
        public IHttpActionResult AddPost([FromBody] Post post)
        {
            //Will Validate according to data annotations specified on model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Posts.Add(post);
            db.SaveChanges();

            return Ok(post.PostId);
        }

        /// <summary>
        /// Deletes a Post from the database
        /// </summary>
        /// <param name="id">The id of the Post to delete.</param>
        /// <returns>200 if successful. 404 if not successful.</returns>
        /// <example>
        /// POST: api/PostData/DeletePost/2
        /// </example>
        [HttpPost]
        public IHttpActionResult DeletePost(int id)
        {
            Post Post = db.Posts.Find(id);
            if (Post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(Post);
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

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.PostId == id) > 0;
        }
    }
}