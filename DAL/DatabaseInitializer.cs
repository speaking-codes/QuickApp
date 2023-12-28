// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Core;
using DAL.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //await _context.Database.MigrateAsync().ConfigureAwait(false);
            await SeedDefaultUsersAsync();
            await SeedDemoDataAsync();
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

        private async Task SeedDemoDataAsync()
        {
            if (!await _context.Customers.AnyAsync())// && !await _context.ProductCategories.AnyAsync())
            {
                _logger.LogInformation("Seeding demo data");

                var cust_1 = new Customer
                {
                    FirstName = "Ebenezer",
                    LastName = "Monney",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Professional,
                            Email = "contact@ebenmonney.com"
                        }
                    }
                };

                var cust_2 = new Customer
                {
                    FirstName = "Itachi",
                    LastName="Uchiha",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Professional,
                             Email = "uchiha@narutoverse.com",
                    PhoneNumber = "+81123456789"
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Work,
                                    Location = "Some fictional Address, Street 123, Konoha",
                    City = "Konoha",
                        }
                    }
                };

                var cust_3 = new Customer
                {
                    FirstName = "John",
                    LastName="Doe",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Deliveries = new List<Delivery>{
                      new Delivery{
                          DeliveryType = EnumDeliveryType.Personal,
                          Email = "johndoe@anonymous.com",
                    PhoneNumber = "+18585858",
                      } },
                };

                var cust_4 = new Customer
                {
                    FirstName = "Jane",
                    LastName="Doe",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType= EnumDeliveryType.Personal,
                            Email = "Janedoe@anonymous.com",
                    PhoneNumber = "+18585858",
                        }
                    }
                };

                var cust_5 = new Customer
                {
                    FirstName = "Mario",
                    LastName="Rossi",
                    TaxIdCode = "MR123456",
                    BirthDate = new DateTime(1985, 5, 15),
                    BirthPlace = "Roma",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Personal,
                                                Email = "mario@email.com",
                    PhoneNumber = "+39 123456789"
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Domicile,
                                                Location = "Via Roma 1",
                    City = "Roma",
                    PostalCode = "00100",
                    Province = "RM",
                        }
                    }
                };

                var cust_6 = new Customer
                {
                    FirstName = "Giulia",
                    LastName="Bianchi",
                    TaxIdCode = "GB654321",
                    BirthDate = new DateTime(1990, 8, 25),
                    BirthPlace = "Milano",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Female,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Personal,
                              Email = "giulia@email.com",
                    PhoneNumber = "+39 987654321"
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Residence,
                                                Location = "Via Milano 5",
                    City = "Milano",
                    PostalCode = "20100",
                    Province = "MI",

                        }
                    }
                };

                var cust_7 = new Customer
                {
                    FirstName = "Marco",
                    LastName="Rossi",
                    TaxIdCode = "MR123456",
                    BirthDate = new DateTime(1985, 5, 15),
                    BirthPlace = "Roma",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Personal,
                                                Email = "mario@email.com",
                    PhoneNumber = "+39 123456789",
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Work,
                            Location= "Via Roma 1",
                    City = "Roma",
                    PostalCode = "00100",
                    Province = "RM",
                        }
                    }
                };

                var cust_8 = new Customer
                {
                    FirstName = "Gilda",
                    LastName="Bianchi",
                    TaxIdCode = "GB654321",
                    BirthDate = new DateTime(1990, 8, 25),
                    BirthPlace = "Milano",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Female,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Professional,
                                                Email = "giulia@email.com",
                    PhoneNumber = "+39 987654321",
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType= EnumAddressType.Residence,
                            Location = "Via Milano 5",
                    City = "Milano",
                    PostalCode = "20100",
                    Province = "MI",
                        }
                    }
                };

                var cust_9 = new Customer
                {
                    FirstName = "Laura",
                    LastName="Verdi",
                    TaxIdCode = "LV987654",
                    BirthDate = new DateTime(1992, 3, 10),
                    BirthPlace = "Napoli",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Female,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
{
    new Delivery
    {
        DeliveryType = EnumDeliveryType.Personal,
                            Email = "laura@email.com",
                    PhoneNumber = "+39 555444333",
    }
},
                    Addresses = new List<Address> {
                    new Address
                    {
                        AddressType = EnumAddressType.Domicile,
                        Location="Via Napoli 3",
                    City = "Napoli",
                    PostalCode = "80100",
                    Province = "NA",
                    }
                    }
                };

                var cust_10 = new Customer
                {
                    FirstName = "Luigi",
                    LastName="Gialli",
                    TaxIdCode = "LG333222",
                    BirthDate = new DateTime(1980, 12, 5),
                    BirthPlace = "Torino",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Male,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Professional,
                                Email = "luigi@email.com",
                    PhoneNumber = "+39 333444555",
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Work,
                            Location = "Via Torino 8",
                    City = "Torino",
                    PostalCode = "10100",
                    Province = "TO",
                        }
                    }
                };

                var cust_11 = new Customer
                {
                    FirstName = "Maria",
                    LastName="Rosso",
                    TaxIdCode = "MR567890",
                    BirthDate = new DateTime(1975, 7, 20),
                    BirthPlace = "Palermo",
                    BirthCounty = "Italy",
                    Gender = EnumGender.Female,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Deliveries = new List<Delivery>
                    {
                        new Delivery
                        {
                            DeliveryType = EnumDeliveryType.Personal,
                                                Email = "maria@email.com",
                    PhoneNumber = "+39 777888999",
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            AddressType = EnumAddressType.Domicile,
                            Location= "Via Palermo 10",
                    City = "Palermo",
                    PostalCode = "90100",
                    Province = "PA",

                        }
                    }
                };

                // E così via, fino a customer10 con dati diversi

                _context.Customers.Add(cust_1);
                _context.Customers.Add(cust_2);
                _context.Customers.Add(cust_3);
                _context.Customers.Add(cust_4);
                _context.Customers.Add(cust_5);
                _context.Customers.Add(cust_6);
                _context.Customers.Add(cust_7);
                _context.Customers.Add(cust_8);
                _context.Customers.Add(cust_9);
                _context.Customers.Add(cust_10);
                _context.Customers.Add(cust_11);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Seeding demo data completed");
            }
        }
    }
}
