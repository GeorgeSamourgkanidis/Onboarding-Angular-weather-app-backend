using dotnet_weather_backend.Data;
using dotnet_weather_backend.Models;

namespace dotnet_weather_backend
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Users.Any())
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        UserName = "George",
                        FavoriteCities = new List<FavoriteCity>()
                        { new FavoriteCity() {City="Thessaloniki"},
                          new FavoriteCity() {City="Athens"}
                        }
                    }
                };
                dataContext.Users.AddRange(users);
                dataContext.SaveChanges();
            }
        }
    }
}
