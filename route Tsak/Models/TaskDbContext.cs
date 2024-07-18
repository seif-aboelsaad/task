using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using route_Tsak.Enum;

namespace route_Tsak.Models
{
    public class TaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer constraints
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(c => c.Id);
                // Define navigation property to Orders
                entity.HasMany(c => c.Orders) // ApplicationUser has many Orders
                      .WithOne(o => o.Customer) // Order has one Customer (User)
                      .HasForeignKey(o => o.CustomerId); // Foreign key constraint
            });

            // Orders constraints
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.OrderDate)
                      .IsRequired();
                entity.Property(o => o.TotalAmount)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired(); // Assuming TotalAmount is decimal(18,2)
            });


            // OrderItem constraints
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.OrderItemId);
                entity.Property(oi => oi.Quantity)
                    .IsRequired();
                entity.Property(oi => oi.UnitPrice)
                    .HasColumnType("decimal(18,2)");
                entity.Property(oi => oi.Discount)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId);

                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(oi => oi.ProductId);
            });

            // Product constraints
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)");
                entity.Property(p => p.Stock)
                    .IsRequired();
            });

            // Invoice constraints
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.InvoiceId);
                entity.Property(i => i.InvoiceDate)
                    .IsRequired();
                entity.Property(i => i.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(i => i.Order)
                    .WithMany(o => o.Invoices)
                    .HasForeignKey(i => i.OrderId);
            });

            // User constraints
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(u => u.PasswordHash)
                    .IsRequired();
                entity.Property(u => u.Role)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
