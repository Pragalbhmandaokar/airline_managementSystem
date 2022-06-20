using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public static models.Login login = new models.Login(); 

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]

        public async Task<ActionResult<models.Login>> Register(models.UserModel request)
        {
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            login.Username = request.username;
            login.PasswordHash = passwordHash;
            login.PasswordSalt = passwordSalt;
            
            //string query = @'INSERT INTO [user] (username,password) values([]'

            return Ok(login);
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from [user]";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("airlineAppCon");
            SqlDataReader myReader;
            using(SqlConnection myconn = new SqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myconn.Close();
                }
            }

            return new JsonResult(table);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> Login(models.UserModel request)
        {
            if(login.Username != request.username)
            {
                return BadRequest("User not found.");
            }
              
            if (!verifyPasswordHash(request.password, login.PasswordHash, login.PasswordSalt))
            {
                return BadRequest("Wrong password, Try again");
            }
            return Ok("Login successful");
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512(passwordSalt)) {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
