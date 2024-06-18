using dotnet_weather_backend.Models;

namespace dotnet_weather_backend.Interfaces
{
    public interface IUserService
    {
        string GetMyName();
        bool UserExistsByUserName(string username);
        Task RegisterUser(string username, string passwordHash);
        User GetUserByUsername(string username);

    }
}
