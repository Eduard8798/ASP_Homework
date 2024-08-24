using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Honework_5_BD.Data;
using Honework_5_BD.Models.Entities;

namespace Honework_5_BD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSubscribeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiSubscribeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiSubscribe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscribeModel>>> GetSubscribers()
        {
            return await _context.Subscribers.ToListAsync();
        }

        // GET: api/ApiSubscribe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscribeModel>> GetSubscribeModel(int id)
        {
            var subscribeModel = await _context.Subscribers.FindAsync(id);

            if (subscribeModel == null)
            {
                return NotFound();
            }

            return subscribeModel;
        }

        // PUT: api/ApiSubscribe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscribeModel(int id, SubscribeModel subscribeModel)
        {
            if (id != subscribeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(subscribeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscribeModelExists(id))
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

        // POST: api/ApiSubscribe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubscribeModel>> PostSubscribeModel(SubscribeModel subscribeModel)
        {
            _context.Subscribers.Add(subscribeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscribeModel", new { id = subscribeModel.Id }, subscribeModel);
        }

        // DELETE: api/ApiSubscribe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribeModel(int id)
        {
            var subscribeModel = await _context.Subscribers.FindAsync(id);
            if (subscribeModel == null)
            {
                return NotFound();
            }

            _context.Subscribers.Remove(subscribeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscribeModelExists(int id)
        {
            return _context.Subscribers.Any(e => e.Id == id);
        }
    }
}
