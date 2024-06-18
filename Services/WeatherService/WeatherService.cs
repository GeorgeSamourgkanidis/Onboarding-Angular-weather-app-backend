using dotnet_weather_backend.Data;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
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
        public async Task<IEnumerable<string>> GetAllFavoriteCityNames(string username)
        {
            return await _dBContext.FavoriteCities.Where(fc => fc.UserUserName == username).Select(fc => fc.City).ToListAsync();
        }
        public bool FavoriteCityExistsByName(string cityName, string username)
        {
            return _dBContext.FavoriteCities.Any(fc => fc.City == cityName && fc.UserUserName == username);
        }
        public async Task SaveFavoriteCity(string cityName, string username)
        {
            _dBContext.Add(new FavoriteCity() { City = cityName, UserUserName = username });
            await _dBContext.SaveChangesAsync();
        }

        public async Task UnsaveFavoriteCity(string cityName, string username)
        {
            _dBContext.Remove(_dBContext.FavoriteCities.First(fc => fc.City == cityName && fc.UserUserName == username));
            await _dBContext.SaveChangesAsync();
        }
    }
}
