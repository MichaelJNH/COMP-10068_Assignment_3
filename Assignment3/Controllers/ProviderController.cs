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
    public class ProviderController : ControllerBase
    {
        private readonly Assignment3Context _context;

        public ProviderController(Assignment3Context context)
        {
            _context = context;
        }

        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(Guid id)
        {
            var provider = await _context.Provider.FindAsync(id);

            if (provider == null)
            {
                throw new StatusException(404, "No provider with that ID exists.");
            }

            return provider;
        }

        // GET: api/Provider?firstName=Michael
        // GET: api/Provider?lastName=Helbert
        // GET: api/Provider?licenseNumber=CAJA723
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProvider([FromQuery(Name = "firstName")] string? firstName, [FromQuery(Name = "lastName")] string? lastName, [FromQuery(Name = "licensePlate")] uint? licensePlate)
        {
            List<Provider> results = null;

            if (firstName != null)
                results = await _context.Provider.Where(p => p.FirstName == firstName).ToListAsync();
            else if (lastName != null)
                results = await _context.Provider.Where(p => p.LastName == lastName).ToListAsync();
            else if (licensePlate != null)
                results = await _context.Provider.Where(p => p.LicenseNumber == licensePlate).ToListAsync();

            if (results == null)
                throw new StatusException(400, "Search query missing.");
            else if (results.Count == 0)
                throw new StatusException(404, "No providers were found.");

            return results;
        }

        // PUT: api/Provider/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(Guid id, Provider provider)
        {
            if (id != provider.Id)
            {
                throw new StatusException(400, "Attempted to PUT provider with wrong Id");
            }

            _context.Entry(provider).State = EntityState.Modified;
            _context.Entry(provider).Property(x => x.CreationTime).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
                {
                    throw new StatusException(404, "No provider with that ID exists.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Provider
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
            provider.CreationTime = DateTimeOffset.Now;
            _context.Provider.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvider", new { id = provider.Id }, provider);
        }

        // DELETE: api/Provider/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Provider>> DeleteProvider(Guid id)
        {
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                throw new StatusException(404, "No provider with that ID exists.");
            }

            _context.Provider.Remove(provider);
            await _context.SaveChangesAsync();

            return provider;
        }

        private bool ProviderExists(Guid id)
        {
            return _context.Provider.Any(e => e.Id == id);
        }
    }
}
