using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Models;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestlinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestlinesController(AppDbContext context)
        {
            _context = context;
        }




        // GET: api/Requestlines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requestline>>> GetRequestline()
        {
            return await _context.Requestline.ToListAsync();
        }

        // GET: api/Requestlines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Requestline>> GetRequestline(int id)
        {
            var requestline = await _context.Requestline.FindAsync(id);

            if (requestline == null)
            {
                return NotFound();
            }

            return requestline;
        }

        // PUT: api/Requestlines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestline(int id, Requestline requestline)
        {
            if (id != requestline.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestlineExists(id))
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

        // POST: api/Requestlines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Requestline>> PostRequestline(Requestline requestline)
        {
            _context.Requestline.Add(requestline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestline", new { id = requestline.Id }, requestline);
        }

        // DELETE: api/Requestlines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestline(int id)
        {
            var requestline = await _context.Requestline.FindAsync(id);
            if (requestline == null)
            {
                return NotFound();
            }

            _context.Requestline.Remove(requestline);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestlineExists(int id)
        {
            return _context.Requestline.Any(e => e.Id == id);
        }
    }
}
