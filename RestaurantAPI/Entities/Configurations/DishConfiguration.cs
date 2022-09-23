using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestaurantAPI.Entities.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> eb)
        {
            eb.Property(d => d.Name)
                .IsRequired();

            eb.Property(d => d.Price)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
        }
    }
}
