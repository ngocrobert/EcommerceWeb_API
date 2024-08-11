using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Product.Core;
using Product.Core.Entities;
using Product.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUsers>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        public DbSet<Address> Address { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.Entity<Address>()
            //    .HasOne(a => a.AppUsers)
            //    .WithOne(b => b.Address)
            //    .HasForeignKey<AppUsers>(b => b.AddressId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<AppUsers>()
            //    .HasOne(b => b.Address)
            //    .WithOne(a => a.AppUsers)
            //    .HasForeignKey<Address>(a => a.AppUserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUsers>()
                .HasOne(a => a.Address)
                .WithOne(b => b.AppUsers)
                .HasForeignKey<Address>(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeliveryMethod>().HasKey(d => d.ID);

            modelBuilder.Entity<OrderItems>()
        .Property(o => o.Price)
        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItems>()
                .Property(o => o.Quantity)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItems>()
                .HasKey(o => o.OrderItemsId);
        }

    }
}
