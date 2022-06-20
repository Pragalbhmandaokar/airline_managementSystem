using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PassengerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("AddPassenger")]
        public async Task<ActionResult<models.Login>> Register(models.UserModel request)
        {
            
            try
            {
                string sqlDataSource = _configuration.GetConnectionString("airlineAppCon");
                SqlConnection myconn = new SqlConnection(sqlDataSource);
                string query = @"INSERT INTO [passenger] (username,password) values(" + "'" + request.username + "'" + "," + "'" + request.password + "'" + ")";
                myconn.Open();
                SqlCommand cmd1 = new SqlCommand(query, myconn);
                cmd1.ExecuteNonQuery();
                myconn.Close();
                return Ok("Register Successful");
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
        }

       
    }
}
