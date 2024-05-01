using static dotnet_weather_backend.DTO.WeatherApiDtos.HistoryDto;

namespace dotnet_weather_backend.DTO.WeatherApiDtos
{
    public class HistoryDto
    {
        public History forecast { get; set; }

        public class History
        {
            public Historyday[] forecastday { get; set; }
        }

        public class Historyday
        {
            public HistoryDayObj day { get; set; }
            public Hour[] hour { get; set; }
        }

        public class HistoryDayObj
        {
            public float maxtemp_c { get; set; }
        }

        public class Hour
        {
            public float temp_c { get; set; }
        }

    }
}
