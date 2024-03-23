
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
using DAL.Helpers;

namespace DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync(bool isRestart);
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

        public async Task SeedAsync(bool isRestart)
        {
            if (isRestart)
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();
            }

            await SeedDefaultUsersAsync();

            if (isRestart)
            {
                await LoadTypologicalTableData();
                await LoadLearningTableData();
            }
        }


        private async Task SeedDefaultUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "administrator";
                const string editorRoleName = "editor";
                const string agentRoleName = "agent";

                await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
                await EnsureRoleAsync(editorRoleName, "Default user editor", ApplicationPermissions.GetEditorPermissionValues());
                await EnsureRoleAsync(agentRoleName, "Default user agent", ApplicationPermissions.GetAgentPermissionValues());

                await CreateUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@datassicurazioni.com", "+1 (123) 000-0000", new string[] { adminRoleName });
                await CreateUserAsync("editor", "tempP@ss123", "Inbuilt Standard User Editor", "editor@datassicurazioni.com", "+1 (123) 000-0001", new string[] { editorRoleName });
                await CreateUserAsync("agent", "tempP@ss123", "Inbuilt Standard User agent", "agent@datassicurazioni.com", "+1 (123) 000-0000", new string[] { agentRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }
        }

        private async Task EnsureRoleAsync(string roleName, string description, IEnumerable<string> claims)
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
                    await LoadMatrixUserItems();

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
            var insurancePolicyCategories = await _context.InsurancePolicyCategories
                                                          .Where(x => x.IsActive)
                                                          .Select(x => x.InsurancePolicyCategoryCode)
                                                          .ToListAsync();
            var temps = await _context.Temps.ToListAsync();
            var rnd = new Random();
            LearningCustomerPreferences learningCustomerPreferences = null;
            DateTime birthDate;

            foreach (var itemCategory in insurancePolicyCategories)
            {
                foreach (var item in temps)
                {
                    var index = rnd.Next(0, insurancePolicyCategories.Count);

                    learningCustomerPreferences = new LearningCustomerPreferences();
                    learningCustomerPreferences.UserId = item.Id;
                    learningCustomerPreferences.CustomerCode = null;
                    learningCustomerPreferences.Gender = item.Gender;

                    if (DateTime.TryParse(item.BirthDate, out birthDate))
                        learningCustomerPreferences.Age = birthDate.GetAge();

                    learningCustomerPreferences.MaritalStatus = item.MaritalStatusCode;
                    learningCustomerPreferences.FamilyType = item.FamilyTypeCode;
                    learningCustomerPreferences.ChildrenNumbers = item.ChildrenNumbers;
                    learningCustomerPreferences.IncomeType = item.IncomeTypeCode;
                    learningCustomerPreferences.ProfessionType = item.ProfessioneTypeCode;
                    learningCustomerPreferences.Income = item.Income;
                    learningCustomerPreferences.Region = item.RegionCode;
                    learningCustomerPreferences.InsurancePolicyCategory = itemCategory;
                    learningCustomerPreferences.PredictionInsurancePolicyCategory= insurancePolicyCategories[index];

                    await _context.LearningCustomerPreferences.AddAsync(learningCustomerPreferences);
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task LoadMatrixUserItems()
        {
            var learningCustomerPreferences = _context.LearningCustomerPreferences
                                                      .Select(x => new LearningCustomerPreferences
                                                      {
                                                          UserId = x.UserId,
                                                          InsurancePolicyCategory = x.InsurancePolicyCategory,
                                                          PredictionInsurancePolicyCategory = x.PredictionInsurancePolicyCategory
                                                      })
                                                      .ToList();

            foreach (var item in learningCustomerPreferences)
                _context.MatrixUsersItems.Add(new MatrixCustomerInsurancePolicy
                {
                    UserId = item.UserId,
                    InsurancePolicyCategory = item.InsurancePolicyCategory,
                    IsLiked = item.InsurancePolicyCategory == item.PredictionInsurancePolicyCategory
                });

            await _context.SaveChangesAsync();
        }
    }
}
