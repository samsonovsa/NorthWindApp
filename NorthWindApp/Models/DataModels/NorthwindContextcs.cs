using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class NorthwindContextcs: DbContext
    {

        public DbSet<Product> Products { get; set; }

        public NorthwindContextcs(DbContextOptions<NorthwindContextcs> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(ProductConfigure);
        }

        public void ProductConfigure(EntityTypeBuilder<Product> builder)
        {

        }

    }
}
