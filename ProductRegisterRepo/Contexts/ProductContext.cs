using Microsoft.EntityFrameworkCore;
using ProductRegister.Models;


namespace ProductRegister.Contexts
{
    public class ProductContext : DbContext
    {                
        public ProductContext(DbContextOptions<ProductContext> options)
        : base(options)
        {

        }

        public virtual DbSet<Product> Product { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_Products");                
            });
        }
    }
}
