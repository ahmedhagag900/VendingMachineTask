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
                .WithMany(p=>p.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasIndex(p => p.SellerId).IsUnique(false);

        }
    }
}
