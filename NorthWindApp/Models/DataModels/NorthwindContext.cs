using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthWindApp.Models.DataModels
{
    public class NorthwindContext: DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(ProductConfigure);
        }

        public void ProductConfigure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne<Category>(s => s.Category)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.CategoryId);

            builder.HasOne<Supplier>(s => s.Supplier)
                .WithMany(g => g.Products)
                .HasForeignKey(s => s.SupplierId);
        }

    }
}
