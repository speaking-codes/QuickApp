using DAL.Core.Interfaces;
using DAL.Exstensions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class StorageManager : Manager, IStorageManager
    {
        #region Private 

        private IList<ProfessionType> _professionTypeList;
        private IList<MaritalStatusType> _maritalStatusList;
        private IList<Province> _provinceList;
        private IList<IncomeClassType> _incomeClassTypeList;
        private IList<int> _yearList;
        private IList<InsurancePolicyCategory> _insurancePolicyCategoryList;

        private IList<int> getYearList()
        {
            var yearList = new List<int>();
            for (var year = 1915; year <= 2006; year++)
                yearList.Add(year);

            return yearList;
        }

        private IList<string> fillEmptyValue(IList<string> lineSplit)
        {
            var rnd = new Random();
            var indexRnd = 0;
            var annualGrossIncomeBase = 0.0;

            #region Sesso

            if (string.IsNullOrEmpty(lineSplit[0].Trim()))
            {
                if (string.IsNullOrEmpty(lineSplit[4]))
                    lineSplit[0] = "S";
                else
                    lineSplit[0] = (rnd.Next() % 2) == 0 ? "M" : "F";
            }

            #endregion

            #region Data di Nascita

            if (string.IsNullOrEmpty(lineSplit[1]))
            {
                indexRnd = rnd.Next(_yearList.Count);
                var year = _yearList[indexRnd];
                var month = rnd.Next(1, 13);
                var day = 0;

                if (month == 2)
                    day = rnd.Next(1, 29);
                else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    day = rnd.Next(1, 32);
                else
                    day = rnd.Next(1, 31);

                var dataNascita = new DateTime(year, month, day);
                lineSplit[1] = dataNascita.ToString("dd/MM/yyyy");
            }

            #endregion

            #region Stato Civile

            if (string.IsNullOrEmpty(lineSplit[2]))
            {
                indexRnd = rnd.Next(_maritalStatusList.Count);
                lineSplit[2] = _maritalStatusList[indexRnd].MaritalStatusDescription;
            }

            #endregion

            #region Coniuge a Carico

            if (string.IsNullOrEmpty(lineSplit[3]))
            {
                if (_maritalStatusList.FirstOrDefault(x => x.MaritalStatusDescription == lineSplit[2]).IsSingle)
                    lineSplit[3] = System.Boolean.FalseString;
                else
                    lineSplit[3] = (rnd.Next() % 2) == 0 ? System.Boolean.TrueString : System.Boolean.FalseString;
            }

            #endregion

            #region Numero Figli

            if (string.IsNullOrEmpty(lineSplit[4]))
            {
                if (lineSplit[0] == "0" || lineSplit[0] == "X" || lineSplit[0] == "S")
                    lineSplit[4] = "0";
                else
                    lineSplit[4] = (rnd.Next() % 4).ToString();
            }

            #endregion

            #region Numero Figli a Carico

            if (string.IsNullOrEmpty(lineSplit[5]))
            {
                var dataNascita = DateTime.Parse(lineSplit[1]);
                var age = (int)DateTime.Now.Subtract(dataNascita).TotalDays / 365;
                var childrenNumbers = int.Parse(lineSplit[4]);

                if (childrenNumbers == 0 || age < 18)
                    lineSplit[5] = "0";
                else
                    lineSplit[5] = rnd.Next(0, childrenNumbers + 1).ToString();
            }

            #endregion

            #region Professione

            if (lineSplit[6] == "Società" || lineSplit[6] == @"Societ�")
                lineSplit[6] = "Societa'";

            #endregion

            #region RAL

            ProfessionType professionType = null;
            if (string.IsNullOrEmpty(lineSplit[7]))
            {
                professionType = _professionTypeList.FirstOrDefault(x => x.ProfessionTypeDescription == lineSplit[6]);

                if ((rnd.Next() % 2) == 0)
                    annualGrossIncomeBase = professionType.MinAnnualGrossIncome * (1 + rnd.NextSingle());
                else
                    annualGrossIncomeBase = professionType.MaxAnnualGrossIncome * rnd.NextSingle();

                if (annualGrossIncomeBase < professionType.MinAnnualGrossIncome)
                    annualGrossIncomeBase = professionType.MinAnnualGrossIncome;

                lineSplit[7] = annualGrossIncomeBase.ToString();
            }

            #endregion

            #region Tipo Reddito

            if (string.IsNullOrEmpty(lineSplit[8]))
                lineSplit[8] = professionType.IncomeType.IncomeTypeDescription;

            #endregion

            #region Provincia

            if (string.IsNullOrEmpty(lineSplit[10]))
            {
                indexRnd = rnd.Next(0, _provinceList.Count);
                lineSplit[10] = _provinceList[indexRnd].ProvinceAbbreviation;
            }
            else
            {
                indexRnd = _provinceList.IndexOf(_provinceList.Where(x => x.ProvinceAbbreviation == lineSplit[10]).FirstOrDefault());
            }

            #endregion

            #region Region

            if (indexRnd >= _provinceList.Count || indexRnd < 0)
                indexRnd = 0;

            lineSplit[9] = _provinceList[indexRnd].ProvinceAbbreviation;
            lineSplit[10] = _provinceList[indexRnd].Region.RegionName;

            #endregion

            return lineSplit;
        }

        private CustomerLearningFeature getCustomerLearningFeature(IList<string> lineSplit)
        {
            var customerLearningFeature = new CustomerLearningFeature();

            customerLearningFeature.Gender = lineSplit[0].Trim();

            var birthDate = DateTime.Parse(lineSplit[1]);
            customerLearningFeature.BirthMonth = birthDate.Month.ToString();
            customerLearningFeature.YearBirth = birthDate.Year.ToString();

            customerLearningFeature.MaritalStatus = lineSplit[2].Trim();
            customerLearningFeature.IsSingle = _maritalStatusList.First(x => x.MaritalStatusDescription == customerLearningFeature.MaritalStatus).IsSingle;
            customerLearningFeature.IsDependentSpouse = bool.Parse(lineSplit[3]);
            customerLearningFeature.ChildrenNumbers = int.Parse(lineSplit[4]);
            customerLearningFeature.DependentChildrenNumber = int.Parse(lineSplit[5]);

            customerLearningFeature.ProfessionType = lineSplit[6].Trim();

            var profession = _professionTypeList.FirstOrDefault(x => x.ProfessionTypeDescription == customerLearningFeature.ProfessionType);
            customerLearningFeature.IsFreelancer = profession.IsFreelancer ?? false;

            var annualGrossIncome = double.Parse(lineSplit[7]);
            var incomeClassType = _incomeClassTypeList.FirstOrDefault(x => x.MinAnnualGrossIncome <= annualGrossIncome && (!x.MaxAnnualGrossIncome.HasValue || x.MaxAnnualGrossIncome >= annualGrossIncome));
            customerLearningFeature.IncomeClassType = (incomeClassType != null) ? incomeClassType.DescriptionIncomeClass : string.Empty;

            customerLearningFeature.IncomeType = lineSplit[8].Trim();
            customerLearningFeature.Country = lineSplit[9].Trim();
            customerLearningFeature.Region = lineSplit[10].Trim();
            customerLearningFeature.InsurancePolicyName = lineSplit[11].Trim();

            return customerLearningFeature;
        }

        private CustomerLearningFeature getInsurancePolicyData(CustomerLearningFeature customerLearningFeature)
        {
            var insurancePolicyCategory = _insurancePolicyCategoryList.FirstOrDefault(x => x.InsurancePolicyCategoryName.ToLower() == customerLearningFeature.InsurancePolicyName.ToLower());

            customerLearningFeature.InsurancePolicyId = insurancePolicyCategory.Id;
            customerLearningFeature.InsurancePolicyCode = insurancePolicyCategory.InsurancePolicyCategoryCode;
            customerLearningFeature.InsurancePolicyDescription = insurancePolicyCategory.InsurancePolicyCategoryDescription;
            customerLearningFeature.WarrantyAvaibles = string.Join(",", insurancePolicyCategory.WarrantyAvaibles.Select(x => x.WarrantyName.Trim()).ToList());

            if (customerLearningFeature.WarrantyAvaibles.Length > 8000)
                customerLearningFeature.WarrantyAvaibles = customerLearningFeature.WarrantyAvaibles.Substring(0, 7990);
            return customerLearningFeature;
        }

        private CustomerLearningFeature fillCustomerLearningFeature(string line)
        {
            IList<string> lineSplit = line.Split(new char[] { ';' }).ToList();
            lineSplit = fillEmptyValue(lineSplit);
            var customerLearningFeature = getCustomerLearningFeature(lineSplit);
            customerLearningFeature = getInsurancePolicyData(customerLearningFeature);
            return customerLearningFeature;
        }

        #endregion

        public StorageManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _professionTypeList = UnitOfWork.ProfessionTypes.GetProfessionTypes().ToList();
            _maritalStatusList = UnitOfWork.MaritalStatusTypes.GetAll();
            _provinceList = UnitOfWork.Provinces.GetProvinces().ToList();
            _incomeClassTypeList = UnitOfWork.IncomeClassTypes.GetAll();
            _yearList = getYearList();
            _insurancePolicyCategoryList = unitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories().ToList();
        }

        public IList<CustomerLearningFeature> LoadDataFromStorage(string inputPath, string processedPath)
        {
            var customerLearningFeatures = new List<CustomerLearningFeature>();
            var pathFiles = Directory.GetFiles(inputPath, "*.csv");

            foreach (var item in pathFiles)
            {
                using (var sr = new StreamReader(item))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                            customerLearningFeatures.Add(fillCustomerLearningFeature(line));
                    }
                }

                File.Move(item, $"{processedPath}{Path.GetFileName(item)}");
            }
            return customerLearningFeatures;
        }

        public async Task SaveDataFeaturs(IList<CustomerLearningFeature> customerLearningFeatures)
        {
            try
            {
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in customerLearningFeatures)
                    UnitOfWork.CustomerLearningFeatures.Add(item);

                await UnitOfWork.SaveChangesAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();

                throw;
            }
        }

        public IList<CustomerLearningFeature> LoadDataFeatures()
        {
            var customerLearningFeatures = UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatures()
                                                           .Where(x => !x.CustomerId.HasValue)
                                                           .Select(x => new CustomerLearningFeature
                                                           {
                                                               Gender = x.Gender,
                                                               BirthMonth = x.BirthMonth,
                                                               YearBirth = x.YearBirth,
                                                               MaritalStatus = x.MaritalStatus,
                                                               IsSingle = x.IsSingle,
                                                               IsDependentSpouse = x.IsDependentSpouse,
                                                               ChildrenNumbers = x.ChildrenNumbers,
                                                               DependentChildrenNumber = x.DependentChildrenNumber,
                                                               ProfessionType = x.ProfessionType,
                                                               IsFreelancer = x.IsFreelancer,
                                                               IncomeClassType = x.IncomeClassType,
                                                               IncomeType = x.IncomeType,
                                                               Country = x.Country,
                                                               Region = x.Region
                                                           })
                                                           .Distinct()
                                                           .ToList();

            long customerId;
            if (UnitOfWork.CustomerLearningFeatures.HasCustomerIds())
                customerId = UnitOfWork.CustomerLearningFeatures.GetMaxCustomerId() + 1;
            else
                customerId = 1;
            foreach (var item in customerLearningFeatures)
            {
                item.CustomerId = customerId;
                customerId++;
            }

            return customerLearningFeatures;
        }

        public async Task UpdateDataFeatures(IList<CustomerLearningFeature> customerLearningFeatures)
        {
            try
            {
                long rowCount = 0;
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in customerLearningFeatures)
                {
                    rowCount += UnitOfWork.CustomerLearningFeatures.CustomUpdate(item);

                    if (rowCount > 12000)
                        break;
                }

                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task ReduceDataFeaturesDuplicated()
        {
            try
            {
                await UnitOfWork.BeginTransactionAsync();

                var customerLearningFeatures = await UnitOfWork.CustomerLearningFeatures.GetCustomCustomerLearningFeatures();
                foreach (var item in customerLearningFeatures)
                {
                    var duplicateCustomerLearningFeatures = await UnitOfWork.CustomerLearningFeatures.GetCustomerLearningFeatures(item.CustomerId, item.InsurancePolicyId);
                    duplicateCustomerLearningFeatures.RemoveAt(0);

                    foreach (var itemDuplicated in duplicateCustomerLearningFeatures)
                        UnitOfWork.CustomerLearningFeatures.CustomDelete(itemDuplicated.Id);
                }

                await UnitOfWork.CommitTransactionAsync();
            }
            catch
            {
                UnitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task AddToStorage(string inputPath, Customer customer)
        {
            var pathFiles = Directory.GetFiles(inputPath, "*.csv");
            var pathFile = string.Empty;

            if (pathFiles.Length > 0)
                pathFile = pathFiles[0];
            else
                pathFile = $"{inputPath}CustomerLearningFeature_{DateTime.Now.ToString("yyyyMMdd")}.csv";

            using (var sw = new StreamWriter(pathFile, true))
            {
                var customerFull = UnitOfWork.Customers.GetCustomerForLearningFeature(customer.CustomerCode).FirstOrDefault();
                if (customerFull != null)
                {
                    var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories(customer.CustomerCode).ToList();
                    if (insurancePolicyCategories.Count == 0)
                        await sw.WriteLineAsync(customerFull.ToCsv(null));

                    foreach (var itemPolicyCategory in insurancePolicyCategories)
                        await sw.WriteLineAsync(customerFull.ToCsv(itemPolicyCategory));
                }
            }
        }

        public async Task<IList<CustomerLearningFeature>> LoadCustomerLearningFeature(long customerId) =>
            await UnitOfWork.CustomerLearningFeatures
                            .GetCustomerLearningFeatures()
                            .Where(x => x.CustomerId.HasValue && x.CustomerId.Value > customerId)
                            //.Where(x =>x.CustomerId.HasValue && x.CustomerId.Value > customerId && x.InsurancePolicyId != 1 && x.InsurancePolicyId != 4 && x.InsurancePolicyId != 6 && x.InsurancePolicyId != 7)
                            .Select(x => new CustomerLearningFeature
                            {
                                CustomerId = x.CustomerId,
                                Gender = x.Gender,
                                BirthMonth = x.BirthMonth,
                                YearBirth = x.YearBirth,
                                MaritalStatus = x.MaritalStatus,
                                IsSingle = x.IsSingle,
                                IsDependentSpouse = x.IsDependentSpouse,
                                ChildrenNumbers = x.ChildrenNumbers,
                                DependentChildrenNumber = x.DependentChildrenNumber,
                                ProfessionType = x.ProfessionType,
                                IsFreelancer = x.IsFreelancer,
                                IncomeClassType = x.IncomeClassType,
                                IncomeType = x.IncomeType,
                                Country = x.Country,
                                Region = x.Region,
                                //InsurancePolicyId= x.InsurancePolicyId,
                                //InsurancePolicyCode= x.InsurancePolicyCode,
                                //InsurancePolicyName = x.InsurancePolicyName,
                                
                            })
                            .Distinct()
                            .OrderBy(x => x.CustomerId)
                            .Take(12000)
                            .ToListAsync();

        public async Task<IList<CustomerLearningFeatureCopy>> LoadCustomerLearningFeatureCopyForTest(long customerId) =>
            await UnitOfWork.CustomerLearningFeatureCopies
                            .GetCustomerLearningFeatures()
                            .Where(x => x.CustomerId > customerId)
                            .Select(x => new CustomerLearningFeatureCopy
                            {
                                CustomerId = x.CustomerId,
                                Gender = x.Gender,
                                BirthMonth = x.BirthMonth,
                                YearBirth = x.YearBirth,
                                MaritalStatus = x.MaritalStatus,
                                IsSingle = x.IsSingle,
                                IsDependentSpouse = x.IsDependentSpouse,
                                ChildrenNumbers = x.ChildrenNumbers,
                                DependentChildrenNumber = x.DependentChildrenNumber,
                                ProfessionType = x.ProfessionType,
                                IsFreelancer = x.IsFreelancer,
                                IncomeClassType = x.IncomeClassType,
                                IncomeType = x.IncomeType,
                                Country = x.Country,
                                Region = x.Region
                            })
                            .Distinct()
                            .ToListAsync();

        public async Task<long> GetLastUserIdFromMatrixUserItems()
        {
            long userId = 0;
            if (await UnitOfWork.MatrixUsersItems.HasItems())
                userId = await UnitOfWork.MatrixUsersItems.GetLastUserId();

            return userId;
        }
        public async Task SaveMatrixUsersItems(IEnumerable<MatrixUsersItems> matrixUsersItems)
        {
            try
            {
                await UnitOfWork.BeginTransactionAsync();

                foreach (var item in matrixUsersItems)
                    UnitOfWork.MatrixUsersItems.Add(item);

                await UnitOfWork.SaveChangesAsync();
                await UnitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();

                throw;
            }
        }

        public override void Dispose()
        {
            if (UnitOfWork.IsTransactionOpened)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
