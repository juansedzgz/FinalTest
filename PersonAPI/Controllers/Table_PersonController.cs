using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PersonAPI.Models;

namespace PersonAPI.Controllers
{
    [RoutePrefix("api/Table_Person")]
    public class Table_PersonController : ApiController
    {
        private P_Model db = new P_Model();

        // GET: api/Table_Person
        [HttpGet]
        [Route("")]
        public IQueryable<Table_Person> GetTable_Person()
        {
            return db.Table_Person;
        }

        // GET: api/Table_Person/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(Table_Person))]
        public IHttpActionResult GetTable_Person(int id)
        {
            Table_Person table_Person = db.Table_Person.Find(id);
            if (table_Person == null)
            {
                return NotFound();
            }

            return Ok(table_Person);
        }

        // GET: api/Table_Person/doctor
        [HttpGet]
        [Route("doctor")]
        [ResponseType(typeof(Table_Person))]
        public IHttpActionResult GetTable_Doctor()
        {
            var table_Person = db.Table_Person
        .Where(p=> p.Type == "Medico")
        .ToList();
            if (table_Person == null)
            {
                return NotFound();
            }

            return Ok(table_Person);
        }

        // GET: api/Table_Person/doctor
        [HttpGet]
        [Route("patient")]
        [ResponseType(typeof(Table_Person))]
        public IHttpActionResult GetTable_Patient()
        {
            var table_Person = db.Table_Person
        .Where(p => p.Type == "Paciente") 
        .ToList();
            if (table_Person == null)
            {
                return NotFound();
            }

            return Ok(table_Person);
        }

        // PUT: api/Table_Person/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable_Person(int id, Table_Person table_Person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Person.Id)
            {
                return BadRequest();
            }

            db.Entry(table_Person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_PersonExists(id))
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

        // POST: api/Table_Person
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Table_Person))]
        public IHttpActionResult PostTable_Person(Table_Person table_Person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Person.Add(table_Person);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = table_Person.Id }, table_Person);
        }

        // DELETE: api/Table_Person/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Table_Person))]
        public IHttpActionResult DeleteTable_Person(int id)
        {
            Table_Person table_Person = db.Table_Person.Find(id);
            if (table_Person == null)
            {
                return NotFound();
            }

            db.Table_Person.Remove(table_Person);
            db.SaveChanges();

            return Ok(table_Person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_PersonExists(int id)
        {
            return db.Table_Person.Count(e => e.Id == id) > 0;
        }
    }
}