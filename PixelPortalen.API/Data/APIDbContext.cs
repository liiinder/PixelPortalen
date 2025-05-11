using Microsoft.EntityFrameworkCore;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Data
{
    public class APIDbContext : DbContext
    {
        public DbSet<AddressInfo> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CustomerRating> CustomerRatings { get; set; }

        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<CustomerRating>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.ProductId, od.OrderId });

            modelBuilder.Entity<Genre>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(g => g.Email)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Genres)
                .WithMany(g => g.Products)
                .UsingEntity("ProductGenre");
            //.OnDelete(DeleteBehavior.ClientCascade);

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.Genres)
            //    .WithMany(g => g.Products)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ProductGenre",
            //        j => j
            //            .HasOne<Genre>()
            //            .WithMany()
            //            .HasForeignKey("GenreId")
            //            .OnDelete(DeleteBehavior.Cascade),  // optional: cascade if Genre is deleted
            //        j => j
            //            .HasOne<Product>()
            //            .WithMany()
            //            .HasForeignKey("ProductId")
            //            .OnDelete(DeleteBehavior.Cascade)); //
            // Detta borde skapa en many to many relation
            // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#basic-many-to-many
        }
    }
}
