using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineShop.Data;
using OnlineShop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class userController : ControllerBase
    {
        public IConfiguration _configuration;
        public userController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<dynamic> Login([FromBody] Object userData)
        {
            var credentials = JsonConvert.DeserializeObject<dynamic>(userData.ToString());
            string username = credentials.username.ToString();
            string password = credentials.password.ToString();

            List<userModel> usersList = await getUsers();
            var user = usersList.Where(x => x.username == username && x.password == password).FirstOrDefault();

            if (user == null)
            {
                return new
                {
                    success = false,
                    message = "Incorrect credentials",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<JwtModel>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.id.ToString()),
                new Claim("username", user.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwt.issuer, jwt.audience, claims, expires:DateTime.Now.AddMinutes(60), signingCredentials:signin);

            return new
            {
                success = true,
                message = "Login successful",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        /*
        public async Task<dynamic> Login([FromBody] Object userData)
        {
            var credentials = JsonConvert.DeserializeObject<dynamic>(userData.ToString());
            string username = credentials.username.ToString();
            string password = credentials.password.ToString();

            List<userModel> usersList = await getUsers();
            var user = usersList.Where(x => x.username == username && x.password == password).FirstOrDefault();

            if(user == null)
            {
                return new
                {
                    success = false,
                    message = "Incorrect credentials",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "Login successful",
                result = user
            };
        }
        */

        [HttpGet]
        public async Task<List<userModel>> getUsers()
        {
            var function = new UserData();
            var list = await function.GetUsers();
            return list;
        }

        /*
        [HttpGet]
        public async Task<ActionResult<List<userModel>>> getUsers()
        {
            var function = new UserData();
            var list = await function.GetUsers();
            return list;
        }

        [HttpPost]
        public async Task postUser([FromBody] userModel parameters)
        {
            var function = new UserData();
            await function.postUser(parameters);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> putUser(int id, [FromBody] userModel parameters)
        {
            var function = new UserData();
            parameters.id = id;
            await function.putUser(parameters);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteUser(int id) 
        {
            var function = new UserData();
            await function.deleteUser(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<userModel>>> getUserById(int id)
        {
            var function = new UserData();
            var list = await function.getUserById(id);
            return list;
        }
        */
    }
}
