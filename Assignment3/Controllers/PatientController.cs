using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly Assignment3Context _context;

        public PatientController(Assignment3Context context)
        {
            _context = context;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            var result = await _context.Patient.FindAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // GET: api/Patient?firstName=Michael
        // GET: api/Patient?lastName=Helbert
        // GET: api/Patiennt?dateOfBirth=1999-06-04
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient([FromQuery(Name = "firstName")] string? firstName, [FromQuery(Name = "lastName")] string? lastName, [FromQuery(Name = "dateOfBirth")] string? dateOfBirth)
        {
            List<Patient> results = null;

            if (firstName != null)
                results = await _context.Patient.Where(p => p.FirstName == firstName).ToListAsync();
            else if (lastName != null)
                results = await _context.Patient.Where(p => p.LastName == lastName).ToListAsync();
            else if (dateOfBirth != null)
                results = await _context.Patient.Where(p => p.DateOfBirth == DateTime.Parse(dateOfBirth)).ToListAsync();

            if (results == null)
                return BadRequest();
            else if (results.Count == 0)
                return NotFound();

            return results;
        }

        // PUT: api/Patient/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;
            _context.Entry(patient).Property(x => x.CreationTime).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Patient
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            patient.CreationTime = DateTimeOffset.Now;
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patient/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(Guid id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
