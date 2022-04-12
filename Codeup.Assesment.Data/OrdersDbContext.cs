using Codeup.Assesment.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeup.Assesment.Data
{
    public partial class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Code);
                entity.ToTable("countries");
                
                entity.Property(e => e.Code).HasColumnName("code");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.ContinentName).HasColumnName("continent_name");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("users");
                
                entity.HasOne(r => r.Country)
                    .WithMany(c => c.Users)
                    .HasForeignKey(r => r.CountryCode);
                
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.CountryCode).HasColumnName("country_code");

            });
            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("merchants");
                
                entity.HasOne(r => r.Country)
                    .WithMany(t => t.Merchants)
                    .HasForeignKey(r => r.CountryCode);
                entity.HasOne(r => r.Admin)
                    .WithMany(t => t.Merchants)
                    .HasForeignKey(r => r.AdminId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CountryCode).HasColumnName("country_code");
                entity.Property(e => e.MerchantName).HasColumnName("merchant_name");
                entity.Property(e => e.CreatedAt)
                        .HasColumnName("created_at")
                        .ValueGeneratedOnAdd();
                entity.Property(e => e.AdminId).HasColumnName("admin_id");

            });

            modelBuilder.Entity<MerchantPeriod>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("merchant_periods");

                entity.HasOne(r => r.Merchant1)
                    .WithOne(t => t.MerchantPeriod1)
                    .HasForeignKey<MerchantPeriod>(r => r.MerchantId);
                entity.HasOne(r => r.Merchant2)
                    .WithOne(t => t.MerchantPeriod2)
                    .HasForeignKey<MerchantPeriod>(r => r.CountryCode)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MerchantId).HasColumnName("merchant_id");
                entity.Property(e => e.CountryCode).HasColumnName("country_code");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.EndDate).HasColumnName("end_date");

            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("products");

                entity.HasOne(r => r.Merchant)
                    .WithMany(t => t.Products)
                    .HasForeignKey(r => r.MerchantId);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.MerchantId).HasColumnName("merchant_id");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt)
                        .HasColumnName("created_at")
                        .ValueGeneratedOnAdd();

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.CreatedAt)
                        .HasColumnName("created_at")
                        .ValueGeneratedOnAdd();

            });
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey("OrderId", "ProductId");
                entity.ToTable("order_items");

                entity.HasOne(r => r.Order)
                     .WithMany(t => t.OrderItems)
                    .HasForeignKey(r => r.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .IsRequired(); 
                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                   .IsRequired();
                entity.Property(e => e.Quantity).HasColumnName("quantity");

            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public virtual DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
