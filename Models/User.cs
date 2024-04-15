namespace dotnet_weather_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public ICollection<FavoriteCity> FavoriteCities { get; set; }
    }
}
