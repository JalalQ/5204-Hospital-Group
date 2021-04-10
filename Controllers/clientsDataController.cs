using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using hospitalPrj.Models;

namespace hospitalPrj.Controllers
{
    public class clientsDataController : ApiController
    {
        private HospitalDbContext db = new HospitalDbContext();


        /// GET : api/clienttData/getClient
        [ResponseType(typeof(IEnumerable<clientDto>))]
        [Route("api/clientData/getClient")]
        public IHttpActionResult getClient()
        {

            List<client> clients = db.client.ToList();
            List<clientDto> clDtos = new List<clientDto> { };
            //Information to expose ApI
            foreach (var cl in clients)
            {
                clientDto NewClient = new clientDto
                {
                    clientId =cl.clientId,
                    clientName = cl.clientName,
                    clientLname = cl.clientLname,
                    healthCN = cl.healthCN,
                   

                };
                clDtos.Add(NewClient);
            }
            return Ok(clDtos);
        }


        // GET: api/clientsData/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Getclient(int id)
        {
            client client = await db.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/clientsData/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putclient(int id, client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.clientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientExists(id))
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

        // POST: api/clientsData
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Postclient(client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.client.Add(client);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = client.clientId }, client);
        }

        // DELETE: api/clientsData/5
        [ResponseType(typeof(client))]
        public async Task<IHttpActionResult> Deleteclient(int id)
        {
            client client = await db.client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.client.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientExists(int id)
        {
            return db.client.Count(e => e.clientId == id) > 0;
        }
    }
}