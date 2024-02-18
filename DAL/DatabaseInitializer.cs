
// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Core;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Core.Helpers;
using System.IO;
using System.Linq;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountManager _accountManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
        {
            _accountManager = accountManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            //await _context.Database.EnsureDeletedAsync();
            //await _context.Database.EnsureCreatedAsync();
            await SeedDefaultUsersAsync();
            //await LoadTypologicalTableData();
            //await LoadLearningTableData();
        }


        private async Task SeedDefaultUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "administrator";
                const string userRoleName = "user";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

                await CreateUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@ebenmonney.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync("user", "tempP@ss123", "Inbuilt Standard User", "user@ebenmonney.com", "+1 (123) 000-0001", new string[] { userRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
            {
                _logger.LogInformation($"Generating default role: {roleName}");

                var applicationRole = new ApplicationRole(roleName, description);

                var result = await _accountManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Succeeded)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");
            }
        }

        private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            _logger.LogInformation($"Generating default user: {userName}");

            var applicationUser = new ApplicationUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true,
                CreatedBy = userName,
                CreatedDate = DateTime.UtcNow
            };

            var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");

            return applicationUser;
        }

        private async Task LoadTypologicalTableData()
        {
            var filePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\DAL\ScriptsInizializzazione\StartupScript.txt";
            using (var reader = new StreamReader(filePath))
            {
                try
                {
                    await _context.Database.BeginTransactionAsync();

                    var line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        await _context.Database.ExecuteSqlRawAsync(line);
                        line = reader.ReadLine();
                    }

                    await GenerateInsurcancePolicyCategoryStatistics();

                    await _context.Database.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        private async Task GenerateInsurcancePolicyCategoryStatistics()
        {
            var insurancePolicyCategories = _context.InsurancePolicyCategories.ToList();
            var startYear = DateTime.Now.Year - 10;
            var endYear = DateTime.Now.Year;
            var startMonth = 1;
            var endMonth = 13;

            var random = new Random();
            for (var currentYear = startYear; currentYear < endYear; currentYear++)
            {
                for (var currentMonth = startMonth; currentMonth < endMonth; currentMonth++)
                {
                    for (var i = 0; i < insurancePolicyCategories.Count; i++)
                    {
                        var statistic = new InsurancePolicyCategoryStatic();
                        statistic.InsurancePolicyCategory = insurancePolicyCategories[i];
                        statistic.Month = (byte)currentMonth;
                        statistic.Year = (short)currentYear;
                        statistic.TotalCount = random.Next(51, 152440);
                        statistic.CreatedBy = null;
                        statistic.UpdatedBy = null;
                        statistic.CreatedDate = DateTime.Now;
                        statistic.UpdatedDate = DateTime.Now;

                        _context.InsurancePolicyCategoryStatics.Add(statistic);
                    }
                }
            }

            endMonth = DateTime.Now.Month;
            for (var currentMonth = startMonth; currentMonth < endMonth; currentMonth++)
            {
                for (var i = 0; i < insurancePolicyCategories.Count; i++)
                {
                    var statistic = new InsurancePolicyCategoryStatic();
                    statistic.InsurancePolicyCategory = insurancePolicyCategories[i];
                    statistic.Month = (byte)currentMonth;
                    statistic.Year = (short)endYear;
                    statistic.TotalCount = random.Next(51, 152440);
                    statistic.CreatedBy = null;
                    statistic.UpdatedBy = null;
                    statistic.CreatedDate = DateTime.Now;
                    statistic.UpdatedDate = DateTime.Now;

                    _context.InsurancePolicyCategoryStatics.Add(statistic);
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task LoadLearningTableData()
        {
            var filePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\DAL\ScriptsInizializzazione\LearningScript.txt";
            using (var reader = new StreamReader(filePath))
            {
                try
                {
                    await _context.Database.BeginTransactionAsync();

                    var line = reader.ReadLine();
                    while (!string.IsNullOrEmpty(line))
                    {
                        await _context.Database.ExecuteSqlRawAsync(line);
                        line = reader.ReadLine();
                    }

                    await LoadLearningTableDataForMatrix();

                    await _context.Database.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _context.Database.RollbackTransactionAsync();
                }
            }
        }

        private async Task LoadLearningTableDataForMatrix()
        {
            var insurancePolicyCategories = await _context.InsurancePolicyCategories.Select(x => x.Id).ToListAsync();
            var temps = await _context.Temps.ToListAsync();
            var rnd = new Random();
            LearningTraining learningTraining = null;
            DateTime dataNascite;
            var minRenewvalNumber = int.MaxValue;
            var maxRenewvalNumber = int.MinValue;
            var current = 0;
            double renewalNumber = 0.0;
            double normalizedRenewalNumber = 0.0;
            foreach (var itemCategory in insurancePolicyCategories)
            {
                foreach (var item in temps)
                {
                    if (string.IsNullOrEmpty(item.Gender))
                        continue;

                    learningTraining = new LearningTraining();
                    learningTraining.CustomerId = item.Id;
                    learningTraining.Gender = item.Gender;

                    if (DateTime.TryParse(item.BirthDate, out dataNascite))
                        learningTraining.Age = (int)Math.Ceiling(DateTime.Now.Subtract(dataNascite).TotalDays / 365);
                    else
                        learningTraining.Age = 0;

                    learningTraining.MaritalStatusId = item.MaritalStatusId ?? 0;
                    learningTraining.FamilyTypeId = item.FamilyTypeId ?? 0;
                    learningTraining.ChildrenNumbers = item.ChildrenNumbers ?? 0;
                    learningTraining.IncomeTypeId = item.IncomeTypeId ?? 0;
                    learningTraining.ProfessionTypeId = item.ProfessionTypeId ?? 0;
                    learningTraining.Income = item.Income ?? 0;
                    learningTraining.RegionId = item.RegionId ?? 0;
                    learningTraining.InsurancePolicyCategoryId = itemCategory;

                    current = rnd.Next(1, 121);
                    if (current < minRenewvalNumber)
                        minRenewvalNumber = current;
                    if (current > maxRenewvalNumber)
                        maxRenewvalNumber = current;
                    learningTraining.RenewalNumber = current;

                    await _context.LearningTrainings.AddAsync(learningTraining);
                }
            }
            await _context.SaveChangesAsync();
            float differenceMaxMin = maxRenewvalNumber - minRenewvalNumber;
            foreach (var item in _context.LearningTrainings)
            {
                renewalNumber= item.RenewalNumber;
                normalizedRenewalNumber = (double)((renewalNumber-minRenewvalNumber)/differenceMaxMin);
                item.NormalizedRenewalNumber = normalizedRenewalNumber;
            }
            await _context.SaveChangesAsync();
        }
    }
}
