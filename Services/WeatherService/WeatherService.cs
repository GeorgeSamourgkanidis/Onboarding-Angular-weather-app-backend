using dotnet_weather_backend.Data;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_weather_backend.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly DataContext _dBContext;
        public WeatherService(DataContext dBContext)
        {
            this._dBContext = dBContext;
        }
        public async Task<IEnumerable<string>> GetAllFavoriteCityNames()
        {
            return await _dBContext.FavoriteCities.Select(fc => fc.City).ToListAsync();
        }
        public bool FavoriteCityExistsByName(string cityName, int userId)
        {
            return _dBContext.FavoriteCities.Any(fc => fc.City == cityName && fc.UserId == userId);
        }
        public bool FavoriteCityExistsById(int cityId)
        {
            return _dBContext.FavoriteCities.Any(fc => fc.Id == cityId);
        }
        public async Task SaveFavoriteCity(string cityName)
        {
            _dBContext.Add(new FavoriteCity() { City = cityName, UserId = 1 });
            await _dBContext.SaveChangesAsync();
        }

        public async Task UnsaveFavoriteCity(string cityName)
        {
            _dBContext.Remove(_dBContext.FavoriteCities.First(fc => fc.City == cityName && fc.UserId == 1));
            await _dBContext.SaveChangesAsync();
        }
    }
}
