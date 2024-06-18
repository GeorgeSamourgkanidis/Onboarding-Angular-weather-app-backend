namespace dotnet_weather_backend.DTO
{
    public class ForecastDto
    {

        public Location location { get; set; }
        public Current current { get; set; }
        public Forecast forecast { get; set; }
    }


    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
    }

    public class Current
    {
        public float temp_c { get; set; }
        public Air_Quality air_quality { get; set; }
    }

    public class Condition
    {
        public string icon { get; set; }
    }

    public class Forecast
    {
        public Forecastday[] forecastday { get; set; }
    }

    public class Forecastday
    {
        public Day day { get; set; }
    }

    public class Day
    {
        public float maxtemp_c { get; set; }
        public float mintemp_c { get; set; }
        public Condition condition { get; set; }
    }

    public class Air_Quality
    {
        public float co { get; set; }
        public float no2 { get; set; }
        public float o3 { get; set; }
        public float so2 { get; set; }
        public float pm2_5 { get; set; }
        public float pm10 { get; set; }
        public int usepaindex { get; set; }
        public int gbdefraindex { get; set; }
    }
}
