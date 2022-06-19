using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static models.Login login = new models.Login(); 
        [HttpPost("register")]

        public async Task<ActionResult<models.Login>> Register(models.UserModel request)
        {
            CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);

            login.Username = request.username;
            login.PasswordHash = passwordHash;
            login.PasswordSalt = passwordSalt;

            return Ok(login);
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
