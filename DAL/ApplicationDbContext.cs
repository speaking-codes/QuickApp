﻿// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------
using DAL.Models;
using DAL.Models.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public string CurrentUserId { get; set; }

        #region Customer

        public DbSet<FamilyType> FamilyTypes { get; set; }
        public DbSet<ProfessionType> ProfessionTypes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Address> Addresses { get; set; }

        #endregion

        #region Municipality

        public DbSet<Area> Areas { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        #endregion

        #region Pet

        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<BreedPetType> BreedPetTypes { get; set; }
        public DbSet<BreedPetDetailType> BreedPetDetailTypes { get; set; }

        #endregion

        #region Car-Byke

        public DbSet<BrandType> BrandTypes { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<PowerType> PowerTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ConfigurationModel> ConfigurationModels { get; set; }

        #endregion

        #region Insurance Policy

        public DbSet<SalesLineType> SalesLineTypes { get; set; }
        public DbSet<InsurancePolicyCategory> InsurancePolicyCategories { get; set; }
        public DbSet<InsurancePolicyCategoryStatic> InsurancePolicyCategoryStatics { get; set; }
        public DbSet<InsurancePolicy> InsurancePolicies { get; set; }
        //public DbSet<VehicleInsurancePolicy> VehicleInsurancePolicies { get; set; }
        //public DbSet<HealthInsurancePolicy> HealthInsurancePolicies { get; set; }
        //public DbSet<WorkActivityInsurancePolicy> WorkActivityInsurancePolicies { get; set; }
        //public DbSet<TravelInsurancePolicy> TravelInsurancePolicies { get; set; }
        //public DbSet<PropertyInsurancePolicy> PropertyInsurancePolicies { get; set; }
        //public DbSet<FamilyInsurancePolicy> FamilyInsurancePolicies { get; set; }
          
        #endregion

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";

            #region User and Role

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

            #endregion

            #region Customer

            builder.Entity<ContractType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ContractType>().ToTable($"App{nameof(ContractTypes)}");

            builder.Entity<GenderType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<GenderType>().ToTable($"App{nameof(GenderTypes)}");

            builder.Entity<MaritalStatusType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<MaritalStatusType>().ToTable($"App{nameof(MaritalStatusTypes)}");

            builder.Entity<FamilyType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<FamilyType>().ToTable($"App{nameof(FamilyTypes)}");

            builder.Entity<ProfessionType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ProfessionType>().ToTable($"App{nameof(ProfessionTypes)}");

            builder.Entity<IncomeType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<IncomeType>().ToTable($"App{nameof(IncomeTypes)}");

            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.FirstName);
            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.LastName);

            builder.Entity<Customer>().Property(c => c.CustomerCode).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.CustomerCode).IsUnique(true);

            builder.Entity<Customer>().HasIndex(c => c.CustomerCode).IsUnique();
            builder.Entity<Customer>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Customer>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Customer>().ToTable($"App{nameof(Customers)}");

            builder.Entity<Delivery>().Property(c => c.Email).HasMaxLength(100).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.PhoneNumber).IsUnicode(false).IsRequired(false).HasMaxLength(30);
            builder.Entity<Delivery>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Delivery>().ToTable($"App{nameof(Deliveries)}");

            builder.Entity<Address>().Property(c => c.Location).HasMaxLength(80).IsRequired(false);
            builder.Entity<Address>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Address>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Address>().ToTable($"App{nameof(Addresses)}");

            #endregion

            #region Municipality

            builder.Entity<Area>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Area>().Property(c => c.AreaName).HasMaxLength(50).IsRequired();
            builder.Entity<Area>().ToTable($"App{nameof(Areas)}");

            builder.Entity<Region>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Region>().Property(c => c.RegionName).HasMaxLength(50).IsRequired();
            builder.Entity<Region>().ToTable($"App{nameof(Regions)}");

            builder.Entity<Province>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Province>().Property(c => c.ProvinceName).HasMaxLength(50).IsRequired();
            builder.Entity<Province>().Property(c => c.ProvinceAbbreviation).HasMaxLength(50).IsRequired();
            builder.Entity<Province>().ToTable($"App{nameof(Provinces)}");

            builder.Entity<Municipality>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Municipality>().Property(c => c.MunicipalityName).HasMaxLength(50).IsRequired();
            builder.Entity<Municipality>().ToTable($"App{nameof(Municipalities)}");

            #endregion

            #region Pet

            builder.Entity<PetType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<PetType>().Property(c => c.PetTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<PetType>().ToTable($"App{nameof(PetTypes)}");

            builder.Entity<BreedPetType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<BreedPetType>().Property(c => c.BreedPetTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<BreedPetType>().ToTable($"App{nameof(BreedPetTypes)}");

            builder.Entity<BreedPetDetailType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<BreedPetDetailType>().Property(c => c.BreedPetDetailTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<BreedPetDetailType>().ToTable($"App{nameof(BreedPetDetailTypes)}");

            #endregion

            #region Car and Byke

            builder.Entity<BrandType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<BrandType>().Property(c => c.BrandTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<BrandType>().ToTable($"App{nameof(BrandTypes)}");

            builder.Entity<ModelType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ModelType>().Property(c => c.ModelTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<ModelType>().ToTable($"App{nameof(ModelTypes)}");

            builder.Entity<PowerType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<PowerType>().Property(c => c.PowerTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<PowerType>().ToTable($"App{nameof(PowerTypes)}");

            //builder.Entity<CarArrangementCylinderType>().Property(c => c.Id).ValueGeneratedNever();
            //builder.Entity<CarArrangementCylinderType>().Property(c => c.ArrangementCylinderDescription).HasMaxLength(50).IsRequired();
            //builder.Entity<CarArrangementCylinderType>().ToTable($"App{nameof(ArrangementCylinderTypes)}");

            //builder.Entity<CarTractionType>().Property(c => c.Id).ValueGeneratedNever();
            //builder.Entity<CarTractionType>().Property(c => c.TractionTypeDescription).HasMaxLength(50).IsRequired();
            //builder.Entity<CarTractionType>().ToTable($"App{nameof(TractionTypes)}");

            //builder.Entity<GearboxType>().Property(c => c.Id).ValueGeneratedNever();
            //builder.Entity<GearboxType>().Property(c => c.GearboxTypeDescription).HasMaxLength(50).IsRequired();
            //builder.Entity<GearboxType>().ToTable($"App{nameof(GearboxTypes)}");

            builder.Entity<Brand>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Brand>().Property(c => c.BrandName).HasMaxLength(100).IsRequired();
            builder.Entity<Brand>().ToTable($"App{nameof(Brands)}");

            builder.Entity<Model>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Model>().Property(c => c.ModelName).HasMaxLength(100).IsRequired();
            builder.Entity<Model>().ToTable($"App{nameof(Models)}");

            builder.Entity<ConfigurationModel>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ConfigurationModel>().Property(c => c.ConfigurationDescription).HasMaxLength(100).IsRequired();
            builder.Entity<ConfigurationModel>().ToTable($"App{nameof(ConfigurationModels)}");

            #endregion

            #region Insurance Policy

            builder.Entity<SalesLineType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<SalesLineType>().Property(c => c.SalesLineCode).HasMaxLength(3).IsFixedLength().IsRequired();
            builder.Entity<SalesLineType>().Property(c => c.BackGroundColor).HasMaxLength(60).IsRequired(false);
            builder.Entity<SalesLineType>().Property(c => c.BackGroundColorCssClass).HasMaxLength(30).IsRequired();
            builder.Entity<SalesLineType>().ToTable($"App{nameof(SalesLineTypes)}");

            builder.Entity<InsurancePolicyCategory>().Property(c => c.Id).ValueGeneratedNever();

            builder.Entity<InsurancePolicyCategory>().Property(c => c.InsurancePolicyCategoryCode).HasMaxLength(10).IsFixedLength(false).IsRequired();
            builder.Entity<InsurancePolicyCategory>().HasIndex(c => c.InsurancePolicyCategoryCode).IsUnique(true);

            builder.Entity<InsurancePolicyCategory>().Property(c => c.InsurancePolicyCategoryName).HasMaxLength(50).IsRequired();
            builder.Entity<InsurancePolicyCategory>().Property(c => c.InsurancePolicyCategoryDescription).HasMaxLength(1000).IsRequired(false);
            builder.Entity<InsurancePolicyCategory>().Property(c => c.IconCssClass).HasMaxLength(30).IsRequired(false);
            builder.Entity<InsurancePolicyCategory>().ToTable($"App{nameof(InsurancePolicyCategories)}");

            builder.Entity<InsurancePolicyCategoryStatic>().ToTable($"App{nameof(InsurancePolicyCategoryStatics)}");

            builder.Entity<InsurancePolicy>().Property(c => c.InsurancePolicyCode).HasMaxLength(150).IsFixedLength(false).IsRequired();
            builder.Entity<InsurancePolicy>().HasIndex(c => c.InsurancePolicyCode).IsUnique(true);

            builder.Entity<InsurancePolicy>().UseTptMappingStrategy().ToTable($"App{nameof(InsurancePolicies)}");

            //builder.Entity<VehicleInsurancePolicy>().ToTable($"App{nameof(VehicleInsurancePolicies)}");
            //builder.Entity<HealthInsurancePolicy>().ToTable($"App{nameof(HealthInsurancePolicies)}");
            //builder.Entity<WorkActivityInsurancePolicy>().ToTable($"App{nameof(WorkActivityInsurancePolicies)}");
            //builder.Entity<TravelInsurancePolicy>().ToTable($"App{nameof(TravelInsurancePolicies)}");
            //builder.Entity<PropertyInsurancePolicy>().ToTable($"App{nameof(PropertyInsurancePolicies)}");
            //builder.Entity<FamilyInsurancePolicy>().ToTable($"App{nameof(FamilyInsurancePolicies)}");
            //builder.Entity<CustomerInsuranceCategoryPolicyRating>().ToTable($"App{nameof(CustomerInsuranceCategoryPolicyRatings)}");

            #endregion


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
