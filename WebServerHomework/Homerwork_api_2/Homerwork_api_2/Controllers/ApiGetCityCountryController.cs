using Homerwork_api_2.Data;
using Homerwork_api_2.Models.Geo;
using Microsoft.AspNetCore.Mvc;

namespace Homerwork_api_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiGetCityCountryController :  ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public ApiGetCityCountryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{AreaId}")]
        public async Task<ActionResult<IEnumerable<CityModel>>> GetCityByArea (int areaId)
        {
            var city =  _context.Cities
                .Where(a => a.AreaId == areaId);

            return city.ToList();
        }
    }
}