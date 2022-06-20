using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

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
            //CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            login.Username =  request.username;
            login.Password = request.password;
           
            try {
                string sqlDataSource = _configuration.GetConnectionString("airlineAppCon");
                SqlConnection myconn = new SqlConnection(sqlDataSource);
                string query = @"INSERT INTO [users] (username,password) values(" +"'"+ request.username + "'"+ "," +"'"+ request.password + "'"+")";
                myconn.Open();
                SqlCommand cmd1 = new SqlCommand(query, myconn);
                cmd1.ExecuteNonQuery();
                myconn.Close();
                return Ok("Register Successful");
            }
            catch(Exception e)
            {
                return BadRequest("Registration Error" + e);
            }
            
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from [users]";
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
            string query = @"select * from [users] where username="+"'"+request.username+"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("airlineAppCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
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
            if(table.Rows.Count > 0){ 
                if (verifyPasswordHash(request.password, table.Rows[0]["password"].ToString()))
                {
              
                    return Ok("Login successful"); 
                }
                return BadRequest("Wrong password, Try again");
            }
            else
            {
                return BadRequest("User not found.");
            }
        }

        private bool verifyPasswordHash(string password, string originalPassword){
            if(password == originalPassword)
            {
                return true;
            }
            return false;
        }
    }
}
