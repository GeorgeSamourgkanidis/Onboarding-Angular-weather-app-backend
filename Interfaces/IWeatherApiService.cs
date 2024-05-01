using dotnet_weather_backend.DTO;
using dotnet_weather_backend.DTO.WeatherApiDtos;

namespace dotnet_weather_backend.Interfaces
{
    public interface IWeatherApiService
    {
        Task<List<SearchValidityDto>> CheckSearchValidity(string cityName);
        Task<ForecastDto> GetTodayForecast(string cityName);
        Task<ForecastDto> GetTodayTomorrowMaxTemps(string cityName);
        Task<HistoryDto> GetYesterdayMaxTemps(string cityName);
    }
}
