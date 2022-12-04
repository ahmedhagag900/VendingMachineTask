using VendingMachine.Core.Entities;
using VendingMachine.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VendingMachine.Infrastructure.EntitiesConfiguration
{
    internal class RoleTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasMany(r => r.Users)
                .WithOne(r => r.Role);

            //builder.HasData(new[]
            //{
            //    new Role{Id=(int)UserRole.Buyer,Name="buyer"},
            //    new Role{Id=(int)UserRole.Seller,Name="seller"},
            //    new Role{Id=(int)UserRole.SA,Name="Supper Admin"}
            //});

        }
    }
}
