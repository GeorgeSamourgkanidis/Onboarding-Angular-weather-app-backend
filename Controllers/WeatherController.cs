using dotnet_weather_backend.Data;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_weather_backend.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly DataContext _dBContext;
        private readonly IWeatherService _weatherService;
        private readonly IUserService _userService;

        public WeatherController(DataContext dBContext, IWeatherService weatherService, IUserService userService)
        {
            this._dBContext = dBContext;
            this._weatherService = weatherService;
            this._userService = userService;
        }

        [HttpGet("favoriteCities")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFavoriteCityNames()
        {
            var username = _userService.GetMyName();
            return Ok(await _weatherService.GetAllFavoriteCityNames(username));
        }


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


    }

}
