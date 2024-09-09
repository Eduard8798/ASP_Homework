using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homerwork_api_2.Data;
using Homerwork_api_2.Models.Geo;

namespace Homerwork_api_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiCountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiCountries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/ApiCountries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountryModel(int id)
        {
            var countryModel = await _context.Countries
                .FirstOrDefaultAsync(c=>c.Id == id);

            if (countryModel == null)
            {
                return NotFound();
            }

            return countryModel;
        }

        // PUT: api/ApiCountries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryModel(int id, CountryModel countryModel)
        {
            if (id != countryModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryModelExists(id))
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

        // POST: api/ApiCountries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryModel>> PostCountryModel(CountryModel countryModel)
        {
            _context.Countries.Add(countryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryModel", new { id = countryModel.Id }, countryModel);
        }

        // DELETE: api/ApiCountries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryModel(int id)
        {
            var countryModel = await _context.Countries.FindAsync(id);
            if (countryModel == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(countryModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryModelExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
