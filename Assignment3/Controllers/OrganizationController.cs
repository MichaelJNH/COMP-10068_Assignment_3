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
    public class OrganizationController : ControllerBase
    {
        private readonly Assignment3Context _context;

        public OrganizationController(Assignment3Context context)
        {
            _context = context;
        }

        // GET: api/Organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganization()
        {
            return await _context.Organization.ToListAsync();
        }

        // GET: api/Organization/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(Guid id)
        {
            var organization = await _context.Organization.FindAsync(id);

            if (organization == null)
            {
                throw new StatusException(404, "No organization with that ID exists.");
            }

            return organization;
        }

        // GET: api/Organization?name=Corpo
        // GET: api/Organization?type=Clinic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetProvider([FromQuery(Name = "name")] string? name, [FromQuery(Name = "type")] string? type)
        {
            List<Organization> results = null;

            if (name != null)
                results = await _context.Organization.Where(o => o.Name == name).ToListAsync();
            else if (type != null)
                results = await _context.Organization.Where(o => o.Type == type).ToListAsync();

            if (results == null)
                throw new StatusException(400, "Missing search query.");
            else if (results.Count == 0)
                throw new StatusException(404, "No organizations were found.");

            return results;
        }

        // PUT: api/Organization/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(Guid id, Organization organization)
        {
            if (id != organization.Id)
            {
                throw new StatusException(400, "Attempted to PUT organization with wrong Id");
            }

            _context.Entry(organization).State = EntityState.Modified;
            _context.Entry(organization).Property(x => x.CreationTime).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    throw new StatusException(404, "No organization with that ID exists.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Organization
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            organization.CreationTime = DateTimeOffset.Now;
            _context.Organization.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganization", new { id = organization.Id }, organization);
        }

        // DELETE: api/Organization/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organization>> DeleteOrganization(Guid id)
        {
            var organization = await _context.Organization.FindAsync(id);
            if (organization == null)
            {
                throw new StatusException(404, "No organization with that ID exists.");
            }

            _context.Organization.Remove(organization);
            await _context.SaveChangesAsync();

            return organization;
        }

        private bool OrganizationExists(Guid id)
        {
            return _context.Organization.Any(e => e.Id == id);
        }
    }
}
