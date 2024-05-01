using dotnet_weather_backend.DTO;
using dotnet_weather_backend.DTO.WeatherApiDtos;
using dotnet_weather_backend.Interfaces;

namespace dotnet_weather_backend.Services.WeatherApiService
{
    public class WeatherApiService : IWeatherApiService
    {
        private IConfiguration _configuration;
        static HttpClient client = new HttpClient();

        public WeatherApiService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<List<SearchValidityDto>> CheckSearchValidity(string cityName)
        {
            List<SearchValidityDto> searchResults = new List<SearchValidityDto>();
            string path = _configuration.GetSection("WeatherApi:url").Value + "/search.json?key=" + _configuration.GetSection("WeatherApi:apiKey").Value + "&q=" + cityName;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                searchResults = await response.Content.ReadFromJsonAsync<List<SearchValidityDto>>();

            }
            return searchResults;
        }

        public async Task<ForecastDto> GetTodayForecast(string cityName)
        {
            ForecastDto forecast = null;
            string path = _configuration.GetSection("WeatherApi:url").Value + "/forecast.json?key=" + _configuration.GetSection("WeatherApi:apiKey").Value + "&q=" + cityName + "&days=1&aqi=no&alerts=no";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadFromJsonAsync<ForecastDto>();

            }
            return forecast;
        }

        public async Task<ForecastDto> GetTodayTomorrowMaxTemps(string cityName)
        {
            ForecastDto forecast = null;
            string path = _configuration.GetSection("WeatherApi:url").Value + "/forecast.json?key=" + _configuration.GetSection("WeatherApi:apiKey").Value + "&q=" + cityName + "&days=2&aqi=yes&alerts=no";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                forecast = await response.Content.ReadFromJsonAsync<ForecastDto>();

            }
            return forecast;
        }

        public async Task<HistoryDto> GetYesterdayMaxTemps(string cityName)
        {
            string yesterdayDate = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            HistoryDto history = null;
            string path = _configuration.GetSection("WeatherApi:url").Value + "/history.json?key=" + _configuration.GetSection("WeatherApi:apiKey").Value + "&q=" + cityName + "&dt=" + yesterdayDate;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                history = await response.Content.ReadFromJsonAsync<HistoryDto>();

            }
            return history;
        }
    }
}
