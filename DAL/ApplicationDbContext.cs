// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------
using DAL.Models;
using DAL.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public string CurrentUserId { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";

            builder.Entity<ApplicationUser>().HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationUser>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<ApplicationUser>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<ApplicationUser>().Property(c => c.Configuration).IsRequired(false);
            builder.Entity<ApplicationUser>().Property(c => c.JobTitle).IsRequired(false);
            builder.Entity<ApplicationUser>().Property(c => c.FullName).IsRequired(false);

            builder.Entity<ApplicationRole>().HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ApplicationRole>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<ApplicationRole>().Property(c => c.UpdatedBy).IsRequired(false);

            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.FirstName);
            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.LastName);
            builder.Entity<Customer>().Property(c => c.CustomerCode).IsRequired().HasMaxLength(16);
            builder.Entity<Customer>().HasIndex(c => c.CustomerCode).IsUnique();
            builder.Entity<Customer>().Property(c => c.BirthPlace).IsRequired(false);
            builder.Entity<Customer>().Property(c => c.BirthCounty).IsRequired(false);
            builder.Entity<Customer>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Customer>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Customer>().ToTable($"App{nameof(Customers)}");

            builder.Entity<Delivery>().Property(c => c.Email).HasMaxLength(100).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.PhoneNumber).IsUnicode(false).IsRequired(false).HasMaxLength(30);
            builder.Entity<Delivery>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Delivery>().ToTable($"App{nameof(Deliveries)}");

            builder.Entity<Address>().Property(c => c.Location).HasMaxLength(80).IsRequired(false);
            builder.Entity<Address>().Property(c => c.Province).HasMaxLength(5).IsRequired(false);
            builder.Entity<Address>().Property(c => c.City).HasMaxLength(50).IsRequired(false);
            builder.Entity<Address>().Property(c => c.PostalCode).HasMaxLength(10).IsRequired(false);
            builder.Entity<Address>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Address>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Address>().ToTable($"App{nameof(Addresses)}");

        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }
    }
}
