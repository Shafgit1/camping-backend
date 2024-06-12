using Microsoft.AspNetCore.Mvc;
using my_weprog_backend.Data;
using my_weprog_backend.Models;

namespace my_weprog_backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CampingController : ControllerBase
    {
        private readonly IDataContext _data;
        private readonly EnumPrices _enp;

        public CampingController(IDataContext data)
        {
            _enp = new EnumPrices(); 
            _data = data;
        }


        [HttpPost("create")]
        public IActionResult Create(Camping model)
        {
            try
            {
                _data.CreateCamping(model);
                return Ok("Camping created successfully");
            }
            catch (System.Exception ex)
            {
                
                return BadRequest("Unable to create the camping.");
            }
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var campings = _data.GetAllCamping();
            return Ok(campings);
        }

        [HttpGet("campingTypesWithPrices")]
        public ActionResult<IDictionary<CampingType, decimal>> GetCampingTypesWithPrices()
        {
            try
            {
                var campingTypesWithPrices = new Dictionary<CampingType, decimal>();
                foreach (CampingType campingType in Enum.GetValues(typeof(CampingType)))
                {
                    decimal price = _enp.GetPriceForCampingType(campingType);
                    campingTypesWithPrices.Add(campingType, price);
                }
                return Ok(campingTypesWithPrices);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error: " + ex.Message);
            }
        }
    }
}