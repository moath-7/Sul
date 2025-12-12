using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Models;

namespace RealEstateManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets - تمثيل الجداول في قاعدة البيانات
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تكوين العلاقات
            // العميل - العقود (One-to-Many)
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Customer)
                .WithMany(cust => cust.Contracts)
                .HasForeignKey(c => c.Customer_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // الوكيل - العقارات (One-to-Many)
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Agent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.Agent_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // الوكيل - العقود (One-to-Many)
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Agent)
                .WithMany(a => a.Contracts)
                .HasForeignKey(c => c.Agent_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // العقار - العقود (One-to-Many)
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Property)
                .WithMany(p => p.Contracts)
                .HasForeignKey(c => c.Property_ID)
                .OnDelete(DeleteBehavior.Cascade);

            // العقد - الدفعات (One-to-Many)
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Contract)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.Contract_ID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}