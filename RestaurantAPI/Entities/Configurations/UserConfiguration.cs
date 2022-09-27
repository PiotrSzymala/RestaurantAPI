using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAPI.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> eb)
        {
            eb.HasOne(u => u.Role)
                .WithOne(r => r.User)
                .HasForeignKey<User>(u => u.RoleId);

            eb.Property(u => u.Email)
                  .IsRequired();
        }
    }
}
