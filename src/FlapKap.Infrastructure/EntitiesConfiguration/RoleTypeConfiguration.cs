﻿using FlapKap.Core.Entities;
using FlapKap.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlapKap.Infrastructure.EntitiesConfiguration
{
    internal class RoleTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasData(new[]
            {
                new Role{Id=(int)UserRoles.Buyer,Name="buyer"},
                new Role{Id=(int)UserRoles.Seller,Name="seller"},
                new Role{Id=(int)UserRoles.SA,Name="Supper Admin"}
            });

        }
    }
}
