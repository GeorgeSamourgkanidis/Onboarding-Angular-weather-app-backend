using Microsoft.AspNetCore.Mvc;

namespace dotnet_weather_backend.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<string>> GetAllFavoriteCityNames(string username);
        bool FavoriteCityExistsByName(string cityName, string username);
        Task SaveFavoriteCity(string cityName, string username);
        Task UnsaveFavoriteCity(string cityName, string username);

    }
}
