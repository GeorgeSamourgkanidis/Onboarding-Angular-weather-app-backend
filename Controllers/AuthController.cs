using dotnet_weather_backend.Data;
using dotnet_weather_backend.DTO;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using dotnet_weather_backend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_weather_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly DataContext _dBContext;

        public AuthController(IConfiguration configuration, IUserService userService, DataContext dBContext)
        {
            this._configuration = configuration;
            this._userService = userService;
            this._dBContext = dBContext;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            if (_userService.UserExistsByUserName(request.UserName))
            {
                return BadRequest("Username already exists");
            }
            await _userService.RegisterUser(request.UserName, passwordHash);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            var user = _userService.GetUserByUsername(request.UserName);
            if (user == null)
            {
                return BadRequest("User not found.");

            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Authentication:JwtToken").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
