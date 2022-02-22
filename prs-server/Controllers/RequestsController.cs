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
    public class RequestsController : ControllerBase {
        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context) {
            _context = context;
        }

        [HttpGet("Review/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsInReview(int userId, Request request)
        {
            var requests = await _context.Requests
                                    .Where(x => x.Status == "REVIEW" //this will narrow the request to only ones in review and not the current users requests
                                     && x.UserId != userId) //this will make it so the reviewers items Do not show up because they may not approve their own requests
                                    .ToListAsync();
            return requests;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest() {
            return await _context.Request.Include(r => r.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id) {
            var request = await _context.Request.Include(x => x.User)
                                        .Include(x => x.Requestlines)
                                        .ThenInclude(xl => xl.Product)
                                        .SingleOrDefaultAsync(x => x.Id == id);
            if (request == null) {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request) {
            if (id != request.Id) {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!RequestExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }
        //this will set the request to review
        [HttpPut("review/{id}")]
        public async Task<IActionResult> SetRequestToReview(int id, Request request) { 
            if (request == null) {
                return BadRequest();
            }
            if (request.Total <= 50) {
                request.Status = "APPROVED";
            } else {
                request.Status = "REVIEW";
            }
            return await PutRequest(id, request);
        }

        //this will set the request to approve
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> SetRequestToApprove(int id, Request request) {
            if (request == null) {
                return BadRequest();
            }
            request.Status = "APPROVED";
            return await PutRequest(id, request);
        }

        //this will set the request to rejected
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> SetRequestToRejected(int id, Request request) {
            if (request == null) {
                return BadRequest();
            }
            request.Status = "REJECTED";
            return await PutRequest(id, request);
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
