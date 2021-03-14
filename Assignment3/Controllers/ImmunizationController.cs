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
    public class ImmunizationController : ControllerBase
    {
        private readonly Assignment3Context _context;

        public ImmunizationController(Assignment3Context context)
        {
            _context = context;
        }

        // GET: api/Immunization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Immunization>> GetImmunization(Guid id)
        {
            var immunization = await _context.Immunization.FindAsync(id);

            if (immunization == null)
            {
                throw new StatusException(404, "No immunization with that ID exists.");
            }

            return immunization;
        }

        // GET: api/Immunization?creationTime=2000-05-06
        // GET: api/Immunization?officialName=ImmuName
        // GET: api/Immunization?tradeName=trName
        // GET: api/Immunization?lotNumber=a123456
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Immunization>>> GetProvider([FromQuery(Name = "officialName")] string? officialName, [FromQuery(Name = "creationTime")] string? creationTime, [FromQuery(Name = "tradeName")] string? tradeName, [FromQuery(Name = "lotNumber")] string? lotNumber)
        {
            List<Immunization> results = null;

            if (officialName != null)
                results = await _context.Immunization.Where(i => i.OfficialName == officialName).ToListAsync();
            else if (creationTime != null)
                results = await _context.Immunization.Where(i => i.CreationTime == DateTime.Parse(creationTime)).ToListAsync();
            else if (tradeName != null)
                results = await _context.Immunization.Where(i => i.TradeName == tradeName).ToListAsync();
            else if (lotNumber != null)
                results = await _context.Immunization.Where(i => i.LotNumber == lotNumber).ToListAsync();

            if (results == null)
                throw new StatusException(400, "Search query missing.");
            else if (results.Count == 0)
                throw new StatusException(404, "No immunizations were found.");

            return results;
        }

        // PUT: api/Immunization/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImmunization(Guid id, Immunization immunization)
        {
            if (id != immunization.Id)
            {
                throw new StatusException(400, "Attempted to PUT immunization with wrong Id");
            }

            immunization.UpdatedTime = DateTimeOffset.Now;
            _context.Entry(immunization).State = EntityState.Modified;
            _context.Entry(immunization).Property(x => x.CreationTime).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImmunizationExists(id))
                {
                    throw new StatusException(404, "No immunization with that ID exists.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Immunization
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Immunization>> PostImmunization(Immunization immunization)
        {
            immunization.CreationTime = DateTimeOffset.Now;
            _context.Immunization.Add(immunization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImmunization", new { id = immunization.Id }, immunization);
        }

        // DELETE: api/Immunization/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Immunization>> DeleteImmunization(Guid id)
        {
            var immunization = await _context.Immunization.FindAsync(id);
            if (immunization == null)
            {
                throw new StatusException(404, "No immunization with that ID exists.");
            }

            _context.Immunization.Remove(immunization);
            await _context.SaveChangesAsync();

            return immunization;
        }

        private bool ImmunizationExists(Guid id)
        {
            return _context.Immunization.Any(e => e.Id == id);
        }
    }
}
