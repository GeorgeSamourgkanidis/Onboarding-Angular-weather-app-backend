using dotnet_weather_backend.Data;
using dotnet_weather_backend.Interfaces;
using dotnet_weather_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_weather_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        private readonly DataContext _dBContext;
        private readonly IWeatherService _weatherService;
        public WeatherController(DataContext dBContext, IWeatherService weatherService)
        {
            this._dBContext = dBContext;
            this._weatherService = weatherService;
        }

        [HttpGet("favoriteCities")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFavoriteCityNames()
        {
            return Ok(await _weatherService.GetAllFavoriteCityNames());
        }


        [HttpPost("saveFavoriteCity")]
        public async Task<IActionResult> SaveFavoriteCity([FromBody] string cityName)
        {
            /*UserId 1 is dummy*/
            if (_weatherService.FavoriteCityExistsByName(cityName, 1))
            {
                return BadRequest("Already exists");
            }
            await _weatherService.SaveFavoriteCity(cityName);
            return Ok();
        }

        [HttpDelete("unsaveFavoriteCity/{cityName}")]
        public async Task<IActionResult> UnsaveFavoriteCity(string cityName)
        {
            /*UserId 1 is dummy*/
            if (!_weatherService.FavoriteCityExistsByName(cityName, 1))
            {
                return BadRequest("City not found");
            }

            await _weatherService.UnsaveFavoriteCity(cityName);
            return Ok();
        }


    }

}
