using dotnet_weather_backend.Data;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using System.Security.Claims;

namespace dotnet_weather_backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dBContext;
        public UserService(DataContext dBContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dBContext = dBContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }

        public bool UserExistsByUserName(string username)
        {
            return _dBContext.Users.Any(user => user.UserName == username);
        }

        public User GetUserByUsername(string username)
        {
            return _dBContext.Users.FirstOrDefault(user => user.UserName == username);
        }

        public async Task RegisterUser(string username, string passwordHash)
        {
            _dBContext.Add(new User() { UserName = username, PasswordHash = passwordHash }); ;
            await _dBContext.SaveChangesAsync();
        }
    }
}
