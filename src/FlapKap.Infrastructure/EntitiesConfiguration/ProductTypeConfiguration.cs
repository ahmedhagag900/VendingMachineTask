using FlapKap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlapKap.Infrastructure.EntitiesConfiguration
{
    internal class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.HasOne(p => p.Seller)
                .WithMany(p=>p.SellerProducts)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Buyers).WithMany(p => p.BuyerProducts);

            builder.HasIndex(p => p.SellerId).IsClustered(false);

        }
    }
}
