namespace dotnet_weather_backend.Models
{
    public class FavoriteCity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string UserUserName { get; set; }
        public User User { get; set; }
    }
}
