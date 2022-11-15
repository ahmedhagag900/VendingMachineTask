using FlapKap.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.EntitiesConfiguration
{
    internal class BuyHistoryTypeConfiguration : IEntityTypeConfiguration<BuyHistory>
    {
        public void Configure(EntityTypeBuilder<BuyHistory> builder)
        {
            builder.HasKey(x => new { x.ProductId, x.UserId, x.BuyDate });

            //builder.


        }
    }
}
