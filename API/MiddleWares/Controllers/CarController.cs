using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebAPI_one.Filters;
using WebAPI_one.ViewModel;

namespace WebAPI_one.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }
        private static Dictionary<int, string> carDictionary = new()
        {
            {0, "BMW"},
            {1, "Mercedes" },
            {2, "Honda" }
        };

        [HttpGet]
        public List<string> GetAll()
        {
            return carDictionary.Values.ToList();
        }

        [HttpGet]
        [Route("{id:int:min(1):max(1000)}")]
        public ActionResult<string> GetById(int id)
        {
            if (!carDictionary.ContainsKey(id))
            {
                return NotFound("This id doesn't exists ");
            }
            return carDictionary[id];
        }

        [HttpPost]
        public ActionResult Add(Car car) // By deafault, search about it in body (complex type)
        {
            /*
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Debugging");
                _logger.LogError($"error happens where id = {car.Id} which is not supported");
                //_logger.LogError(ModelState.ErrorCount.ToString());
                return BadRequest("something went wrong"); 
            }
            */
            
           
            if (carDictionary.ContainsKey(car.Id))
            {
                return BadRequest("This id already exists ");
            }
            carDictionary[car.Id] = car.Name;
            // (custom) return (created status code, response body)
            // return StatusCode(StatusCodes.Status201Created, new { Message = "The car has been added" });
            return CreatedAtAction(
                "GetById",
                new {id = car.Id},
                // response body
                new { Message = "The car has been added" }
                );
        }
        [HttpPost]
        [Route("V2")]
        [CarTypeValidation]
        public ActionResult AddV2(Car car) 
        {

            if (carDictionary.ContainsKey(car.Id))
            {
                return BadRequest("This id already exists ");
            }
            carDictionary[car.Id] = car.Name;  
            return CreatedAtAction(
                "GetById",
                new { id = car.Id },
                // response body
                new { Message = "The car has been added" }
                );
        }


        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Edit(Car car, int id)
        {
            if(car.Id != id) // id of body doesn't equal id of URL 
            {
                BadRequest();
            }

            if (!carDictionary.ContainsKey(id))
            {
                return NotFound();
            }

            carDictionary[id] = car.Name;
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!carDictionary.ContainsKey(id))
            {
                return NotFound();
            }
            carDictionary.Remove(id);
            return NoContent();
        }

    }
}
