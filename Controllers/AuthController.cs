using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using airline_backend.database.Entity;
using airline_backend.models;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        databaseController db;
        public AuthController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(db.users.ToList());
        }

        //localhost:port/api/auth/register

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserModel request)
        {
            try
            {
                var get_user = db.users.FirstOrDefault(p => p.username == request.username);
                if (get_user == null)
                {
                    db.users.Add(request);
                    db.SaveChanges();
                    return Ok("Register Successful");
                }
                else
                {
                    return Ok("Username already exsits");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }

        }
        [HttpPost("login")]

        public async Task<ActionResult<UserModel>> login(UserModel request)
        {
            //CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            try
            {
                var get_user = db.users.FirstOrDefault(p => p.username == request.username && p.password == request.password);
                if (get_user != null)
                {
                    return Ok("login successfull");
                }
                else
                {

                    return Ok("UserName or Password does not match.");
                }
                db.SaveChanges();
                return Ok("Register Successful");
            }
            catch (Exception e)
            {
                return BadRequest("Registration Error" + e);
            }

        }
    }
}
