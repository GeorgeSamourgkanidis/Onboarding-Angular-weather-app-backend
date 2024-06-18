using dotnet_weather_backend.Data;
using dotnet_weather_backend.DTO;
using dotnet_weather_backend.DTO.WeatherApiDtos;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_weather_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly IWeatherApiService _weatherApiService;
        private readonly IUserService _userService;

        public WeatherController(IWeatherService weatherService, IWeatherApiService weatherApiService, IUserService userService)
        {
            this._weatherService = weatherService;
            this._weatherApiService = weatherApiService;
            this._userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("favoriteCities")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFavoriteCityNames()
        {
            var username = _userService.GetMyName();
            return Ok(await _weatherService.GetAllFavoriteCityNames(username));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("saveFavoriteCity")]
        public async Task<IActionResult> SaveFavoriteCity([FromBody] string cityName)
        {
            var username = _userService.GetMyName();
            if (_weatherService.FavoriteCityExistsByName(cityName, username))
            {
                return BadRequest("Already exists");
            }
            await _weatherService.SaveFavoriteCity(cityName, username);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("unsaveFavoriteCity/{cityName}")]
        public async Task<IActionResult> UnsaveFavoriteCity(string cityName)
        {
            var username = _userService.GetMyName();
            if (!_weatherService.FavoriteCityExistsByName(cityName, username))
            {
                return BadRequest("City not found");
            }

            await _weatherService.UnsaveFavoriteCity(cityName, username);
            return Ok();
        }

        [HttpGet("checkSearchValidity/{cityName}")]
        public async Task<ActionResult<SearchValidityDto>> CheckSearchValidity(string cityName)
        {
            return Ok(await _weatherApiService.CheckSearchValidity(cityName));
        }

        [HttpGet("getTodayForecast/{cityName}")]
        public async Task<ActionResult<ForecastDto>> GetTodayForecast(string cityName)
        {
            return Ok(await _weatherApiService.GetTodayForecast(cityName));
        }

        [HttpGet("getTodayTomorrowMaxTemps/{cityName}")]
        public async Task<ActionResult<ForecastDto>> GetTodayTomorrowMaxTemps(string cityName)
        {
            return Ok(await _weatherApiService.GetTodayTomorrowMaxTemps(cityName));
        }

        [HttpGet("getYesterdayMaxTemps/{cityName}")]
        public async Task<ActionResult<HistoryDto>> GetYesterdayMaxTemps(string cityName)
        {
            return Ok(await _weatherApiService.GetYesterdayMaxTemps(cityName));
        }

    }

}
