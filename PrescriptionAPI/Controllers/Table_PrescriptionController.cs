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
using PrescriptionAPI.Models;

namespace PrescriptionAPI.Controllers
{
    [RoutePrefix("api/Table_Prescription")] // Route prefix for the controller
    public class Table_PrescriptionController : ApiController
    {
        private Pre_Model db = new Pre_Model();

        // GET: api/Table_prescription
        [HttpGet]
        [Route("")]
        public IQueryable<Table_Prescription> GetTable_Prescription()
        {
            return db.Table_Prescription;
        }

        // GET: api/Table_prescription/5
        [HttpGet]
        [Route("{id:int}", Name = "GetTablePrescriptionById")]
        [ResponseType(typeof(Table_Prescription))]
        public async Task<IHttpActionResult> GetTable_Prescription(int id)
        {
            Table_Prescription table_Prescription = await db.Table_Prescription.FindAsync(id);
            if (table_Prescription == null)
            {
                return NotFound();
            }

            return Ok(table_Prescription);
        }

        [HttpGet]
        [Route("patient/{id:int}")]
        public async Task<IHttpActionResult> GetPatientTable_Prescription(int id)
        {
            var table_Person = await db.Table_Prescription
        .Where(p => p.PatientId == id)
        .ToListAsync();
            if (table_Person == null)
            {
                return NotFound();
            }

            return Ok(table_Person);
        }

        // PUT: api/Table_prescription/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTable_Prescription(int id, Table_Prescription table_Prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table_Prescription.Id)
            {
                return BadRequest();
            }

            db.Entry(table_Prescription).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Table_PrescriptionExists(id))
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

        // POST: api/Table_prescription
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Table_Prescription))]
        public IHttpActionResult PostTable_Prescription(Table_Prescription table_Prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Table_Prescription.Add(table_Prescription);
            db.SaveChanges();

            return CreatedAtRoute("GetTablePrescriptionById", new { id = table_Prescription.Id }, table_Prescription);
        }

        // DELETE: api/Table_prescription/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Table_Prescription))]
        public async Task<IHttpActionResult> DeleteTable_Prescription(int id)
        {
            Table_Prescription table_Prescription = await db.Table_Prescription.FindAsync(id);
            if (table_Prescription == null)
            {
                return NotFound();
            }

            db.Table_Prescription.Remove(table_Prescription);
            await db.SaveChangesAsync();

            return Ok(table_Prescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Table_PrescriptionExists(int id)
        {
            return db.Table_Prescription.Count(e => e.Id == id) > 0;
        }
    }
}
