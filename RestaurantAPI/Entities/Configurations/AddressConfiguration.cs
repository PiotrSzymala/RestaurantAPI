using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestaurantAPI.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> eb)
        {
            eb.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(75);

            eb.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(75);
        }
    }
}
