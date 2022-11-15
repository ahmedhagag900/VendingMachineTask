using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure
{
    public class VendingMachieneContext:DbContext
    {

        public VendingMachieneContext(DbContextOptions<VendingMachieneContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendingMachieneContext).Assembly);
           base.OnModelCreating(modelBuilder);
        }


    }
}
