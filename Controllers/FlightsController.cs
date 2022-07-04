using airline_backend.database.Entity;
using airline_backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        databaseController db;
        public FlightsController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(db.flights.ToList());
        }

        //{userid: "1",flightName:"name"}

        [HttpPost("flightRegister")]
        public async Task<ActionResult<Flights>> Register(Flights request)
        {
            try
            {
                db.flights.Add(request);
                db.SaveChanges();
                return Ok("Flight added");
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

        [HttpPost("searchFlightByPlace")]
        public async Task<ActionResult<Flights>> getflights(string destinationPlace,string sourcePlace)
        {
            try
            {
                var flightsSearch = db.flights.Where(p => p.destinationPlace == destinationPlace && p.sourcePlace == sourcePlace);
                return new JsonResult(flightsSearch.ToList());
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

        [HttpPost("searchFlight")]
        public async Task<ActionResult<Flights>> getflights(int flightId)
        {
            try
            {
                var flightsSearch = db.flights.Where(p => p.FlightsId == flightId);
                return new JsonResult(flightsSearch.ToList()); 
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

    }
}
