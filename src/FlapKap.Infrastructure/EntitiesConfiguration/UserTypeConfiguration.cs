using FlapKap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlapKap.Infrastructure.EntitiesConfiguration
{
    internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.SellerProducts).WithOne(p => p.Seller);

            builder.HasMany(p => p.BuyerProducts).WithMany(p => p.Buyers);


            builder.HasIndex(p => p.UserName).IsUnique();
            builder.HasIndex(p => p.RoleId).IsClustered(false);

        }
    }
}
