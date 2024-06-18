namespace dotnet_weather_backend.DTO.WeatherApiDtos
{
    public class SearchValidityDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string url { get; set; }

    }
}
