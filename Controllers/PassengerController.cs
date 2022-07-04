using airline_backend.database.Entity;
using airline_backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        databaseController db;
        public PassengerController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IEnumerable<passenger> Get()
        {
            return db.passengers.ToList();
        }
        [HttpPost("AddPassenger")]
        public async Task<ActionResult<passenger>> Register(passenger request)
        {
            var get_user = db.passengers.FirstOrDefault(p => p.panNumber == request.panNumber);
            try
            {
                if (get_user == null)
                {
                    db.passengers.Add(request);
                  
                    db.SaveChanges();
                    return Ok("Register Successful");
                }
                else
                {
                    return Ok(new JsonResult( get_user));
                }
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

        [HttpGet("SearchPassenger")]
        public async Task<ActionResult<passenger>> SearchPassengerByPanCard([FromBody] string panCard)
        {
            var get_user = db.passengers.FirstOrDefault(p => p.panNumber == panCard);
            try
            {
                if (get_user == null)
                {
                    return Ok("passenger not found");
                }
                else
                {
                    return Ok(get_user);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }
    }
}
