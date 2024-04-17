using Microsoft.AspNetCore.Mvc;

namespace dotnet_weather_backend.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<string>> GetAllFavoriteCityNames();
        bool FavoriteCityExistsByName(string cityName, int userId);
        bool FavoriteCityExistsById(int cityId);
        Task SaveFavoriteCity(string cityName);
        Task UnsaveFavoriteCity(string cityName);

    }
}
