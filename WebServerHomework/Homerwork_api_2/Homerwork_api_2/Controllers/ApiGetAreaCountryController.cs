using Homerwork_api_2.Data;
using Homerwork_api_2.Models.Geo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homerwork_api_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApiGetAreaCountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public ApiGetAreaCountryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{countryId}")] //?1
        public async Task<ActionResult<IEnumerable<AreaModel>>> GetAreaByCountry(int countryId)
        {
            var areas =  _context.Areas
                .Where(a => a.CountryId == countryId);

            return areas.ToList();
        }
    }
}