using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWindApp.DTO.Models;

namespace NorthWindApp.DAL.EF
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
