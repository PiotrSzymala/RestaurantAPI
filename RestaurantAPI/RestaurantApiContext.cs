using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantApiContext :DbContext
    {
        public RestaurantApiContext(DbContextOptions<RestaurantApiContext> options) : base(options)
        {
            
        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(eb =>
            {
                eb.HasOne(r => r.Address)
                    .WithOne(a => a.Restaurant)
                    .HasForeignKey<Address>(a=>a.RestaurantId);

                eb.HasMany(r => r.Dishes)
                    .WithOne(d => d.Restaurant)
                    .HasForeignKey(d => d.RestaurantId);

                eb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Dish>(eb =>
            {
                eb.Property(d => d.Name)
                    .IsRequired();

                eb.Property(d => d.Price)
                    .HasColumnType("decimal")
                    .HasPrecision(18,2);
            });

            modelBuilder.Entity<Address>(eb =>
            {
                eb.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(75);

                eb.Property(a=>a.Street)
                    .IsRequired()
                    .HasMaxLength(75);
            });
        }
    }
}
