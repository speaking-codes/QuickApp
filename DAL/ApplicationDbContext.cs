// ---------------------------------------
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
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public string CurrentUserId { get; set; }

        #region Customer

        public DbSet<FamilyType> FamilyTypes { get; set; }
        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<ProfessionType> ProfessionTypes { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        //public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }
        //public DbSet<AgeClassType> AgeClassTypes { get; set; }
        public DbSet<IncomeClassType> IncomeClassTypes { get; set; }

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

        #region Vehicle

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

        public DbSet<WarrantyAvaible> WarrantyAvaibles { get; set; }
        public DbSet<WarrantySelected> WarrantySelecteds { get; set; }

        public DbSet<SportEventType> SportEventTypes { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<LegalProtection> LegalProtections { get; set; }
        public DbSet<LargeBuilding> LargeBuildings { get; set; }
        public DbSet<Injury> Injuries { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Business> Businesss { get; set; }

        public DbSet<KinshipRelationshipType> KinshipRelationshipTypes { get; set; }


        public DbSet<CustomerLearningFeature> CustomerLearningFeatures { get; set; }

        public DbSet<MatrixUsersItems> MatrixUsersItems { get; set; }

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

            //builder.Entity<GenderType>().Property(c => c.Id).ValueGeneratedNever();
            //builder.Entity<GenderType>().ToTable($"App{nameof(GenderTypes)}");

            builder.Entity<MaritalStatusType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<MaritalStatusType>().ToTable($"App{nameof(MaritalStatusTypes)}");

            builder.Entity<FamilyType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<FamilyType>().ToTable($"App{nameof(FamilyTypes)}");

            builder.Entity<IncomeType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<IncomeType>().ToTable($"App{nameof(IncomeTypes)}");

            builder.Entity<ProfessionType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ProfessionType>().ToTable($"App{nameof(ProfessionTypes)}");

            //builder.Entity<AgeClassType>().Property(c => c.Id).ValueGeneratedNever();
            //builder.Entity<AgeClassType>().ToTable($"App{nameof(AgeClassTypes)}");

            builder.Entity<IncomeClassType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<IncomeClassType>().ToTable($"App{nameof(IncomeClassTypes)}");

            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.FirstName);
            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.LastName);

            builder.Entity<Customer>().Property(c => c.CustomerCode).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().HasIndex(c => c.CustomerCode).IsUnique(true);

            builder.Entity<Customer>().HasIndex(c => c.CustomerCode).IsUnique();
            builder.Entity<Customer>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Customer>().Property(c => c.UpdatedBy).IsRequired(false);

            builder.Entity<Customer>().HasOne(c => c.BirthMunicipality)
                                                  .WithMany(m => m.Customers)
                                                  .HasForeignKey(c => c.BirthMunicipalityId)
                                                  .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Customer>().HasOne(c => c.FamilyType)
                                      .WithMany(m => m.Customers)
                                      .HasForeignKey(c => c.FamilyTypeId)
                                      .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>().HasOne(c => c.MaritalStatus)
                                      .WithMany(m => m.Customers)
                                      .HasForeignKey(c => c.MaritalStatusId)
                                      .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>().HasOne(c => c.BirthMunicipality)
                                      .WithMany(m => m.Customers)
                                      .HasForeignKey(c => c.BirthMunicipalityId)
                                      .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>().HasOne(c => c.ContractType)
                                      .WithMany(m => m.Customers)
                                      .HasForeignKey(c => c.ContractTypeId)
                                      .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<Customer>().HasOne(c => c.IncomeType)
            //                          .WithMany(m => m.Customers)
            //                          .HasForeignKey(c => c.IncomeTypeId)
            //                          .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>().HasOne(c => c.ProfessionType)
                                      .WithMany(m => m.Customers)
                                      .HasForeignKey(c => c.ProfessionTypeId)
                                      .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Customer>().ToTable($"App{nameof(Customers)}");

            builder.Entity<Delivery>().Property(c => c.Email).HasMaxLength(100).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.PhoneNumber).IsUnicode(false).IsRequired(false).HasMaxLength(30);
            builder.Entity<Delivery>().Property(c => c.IsPrimary).IsRequired(true).HasDefaultValue(false);
            builder.Entity<Delivery>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Delivery>().Property(c => c.UpdatedBy).IsRequired(false);
            builder.Entity<Delivery>().ToTable($"App{nameof(Deliveries)}");

            builder.Entity<Address>().Property(c => c.Location).HasMaxLength(80).IsRequired(false);
            builder.Entity<Address>().Property(c => c.IsPrimary).IsRequired(true).HasDefaultValue(false);
            builder.Entity<Address>().Property(c => c.CreatedBy).IsRequired(false);
            builder.Entity<Address>().Property(c => c.UpdatedBy).IsRequired(false);

            builder.Entity<Address>().HasOne(c => c.Municipality)
                                    .WithMany(m => m.Addresses)
                                    .HasForeignKey(c => c.MunicipalityId)
                                    .OnDelete(DeleteBehavior.NoAction);

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
            builder.Entity<BrandType>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<BrandType>().ToTable($"App{nameof(BrandTypes)}");

            builder.Entity<ModelType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ModelType>().Property(c => c.ModelTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<ModelType>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<ModelType>().ToTable($"App{nameof(ModelTypes)}");

            builder.Entity<PowerType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<PowerType>().Property(c => c.PowerTypeDescription).HasMaxLength(50).IsRequired();
            builder.Entity<PowerType>().Property(c => c.IsActive).HasDefaultValue(true);
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
            builder.Entity<Brand>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<Brand>().ToTable($"App{nameof(Brands)}");

            builder.Entity<Model>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Model>().Property(c => c.ModelName).HasMaxLength(100).IsRequired();
            builder.Entity<Model>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<Model>().ToTable($"App{nameof(Models)}");

            builder.Entity<ConfigurationModel>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<ConfigurationModel>().Property(c => c.ConfigurationDescription).HasMaxLength(100).IsRequired();
            builder.Entity<ConfigurationModel>().Property(c => c.IsActive).HasDefaultValue(true);
            builder.Entity<ConfigurationModel>().ToTable($"App{nameof(ConfigurationModels)}");

            #endregion

            #region Insurance Policy

            builder.Entity<KinshipRelationshipType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<KinshipRelationshipType>().ToTable($"App{nameof(KinshipRelationshipTypes)}");

            builder.Entity<SportEventType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<SportEventType>().ToTable($"App{nameof(SportEventTypes)}");

            builder.Entity<BusinessType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<BusinessType>().ToTable($"App{nameof(BusinessTypes)}");

            builder.Entity<SalesLineType>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<SalesLineType>().Property(c => c.SalesLineCode).HasMaxLength(5).IsFixedLength().IsRequired();
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
            builder.Entity<InsurancePolicy>().Property(c => c.IsTransferForMachineLearning).IsRequired(true).HasDefaultValue(false);
            builder.Entity<InsurancePolicy>().HasIndex(c => c.InsurancePolicyCode).IsUnique(true);
            builder.Entity<InsurancePolicy>().HasOne(c => c.InsurancePolicyCategory)
                                               .WithMany(m => m.InsurancePolicies)
                                               .HasForeignKey(c => c.InsurancePolicyCategoryId)
                                               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InsurancePolicy>().UseTptMappingStrategy().ToTable($"App{nameof(InsurancePolicies)}");

            builder.Entity<WarrantyAvaible>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<WarrantyAvaible>().ToTable($"App{nameof(WarrantyAvaibles)}");

            builder.Entity<WarrantySelected>().ToTable($"App{nameof(WarrantySelecteds)}");

            builder.Entity<Vehicle>().ToTable($"App{nameof(Vehicles)}");
            builder.Entity<Pet>().ToTable($"App{nameof(Pets)}");
            builder.Entity<SportEvent>().ToTable($"App{nameof(SportEvents)}");
            builder.Entity<House>().ToTable($"App{nameof(Houses)}");
            builder.Entity<LegalProtection>().ToTable($"App{nameof(LegalProtections)}");
            builder.Entity<LargeBuilding>().ToTable($"App{nameof(LargeBuildings)}");
            builder.Entity<Injury>().ToTable($"App{nameof(Injuries)}");
            builder.Entity<Illness>().ToTable($"App{nameof(Illnesses)}");
            builder.Entity<Business>().ToTable($"App{nameof(Businesss)}");


            builder.Entity<CustomerLearningFeature>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<CustomerLearningFeature>().HasKey(c => c.Id);
            builder.Entity<CustomerLearningFeature>().ToTable($"App{nameof(CustomerLearningFeatures)}");

            builder.Entity<MatrixUsersItems>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<MatrixUsersItems>().Property(c => c.UserId).ValueGeneratedNever();
            builder.Entity<MatrixUsersItems>().Property(c => c.ItemId).ValueGeneratedNever();
            builder.Entity<MatrixUsersItems>().HasKey(c => c.Id);
            builder.Entity<MatrixUsersItems>().ToTable($"App{nameof(MatrixUsersItems)}");

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
