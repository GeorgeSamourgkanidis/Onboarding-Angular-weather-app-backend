
using System.ComponentModel.DataAnnotations;

namespace dotnet_weather_backend.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<FavoriteCity> FavoriteCities { get; set; }
    }
}
