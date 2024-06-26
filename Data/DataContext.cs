﻿using dotnet_weather_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace dotnet_weather_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<FavoriteCity> FavoriteCities { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Define Username as primaryKey*/
            modelBuilder.Entity<User>().HasKey(user => user.UserName);

        }


    }
}
