using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceMachineLearningTraining
{
    public class PreProcess
    {
        private List<List<string>> LoadProfession(string pathFile)
        {
            var professioneList = new List<List<string>>();

            using (var sr = new StreamReader(pathFile))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                      var  lineSplit = line.Split(new char[] { ';' }).ToList();
                        professioneList.Add(lineSplit);
                    }
                }
            }

            return professioneList;
        }

        private List<int> LoadYears()
        {
            var yearList = new List<int>();
            for (var year = 1915; year <= 2006; year++)
                yearList.Add(year);

            return yearList;
        }

        private List<string> LoadStatoCivile()
        {
            return new List<string>()
                    {
                        "Divorziato",
                        "Convivente",
                        "Vedovo"
                    };

        }

        public List<List<string>> LoadData(string pathFileCustomer, string pathFileProfessioni)
        {
            var lineSplit = new List<string>();
            var index = -1;
            var rnd = new Random();
            var indexRnd = 0;

            var professioneList = LoadProfession(pathFileProfessioni);
            var yearList = LoadYears();
            var statoCivileList = LoadStatoCivile();
            var customerList = new List<List<string>>();
            double annualGrossIncomeBase = 0.0;

            var liberoProfessionistaList = new List<string> {
                        "Libero professionista",
                        "Commerciante",
                        "Agente di commercio",
                        "Altra professione",
                        "Consulente Commerciale",
                        "Imprenditore",
                        "Lavoratore autonomo"
                    };
            var dipedenteList = new List<string> {
                        "Dipendente generico",
                        "Dipendente Ente privato",
                        "Dipendente Ente pubblico"
                    };
            var nullaTenenteList = new List<string> {
                        "Disoccupato",
                        "Studente",
                        "Pensionato"
                    };
            var provinciaList = new List<string>();

            using (var sr = new StreamReader(pathFileCustomer))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        lineSplit = line.Split(new char[] { ';' }).ToList();

                        index++;
                        lineSplit.Insert(0, index.ToString());

                        if (string.IsNullOrEmpty(lineSplit[1].Trim()))
                        {
                            if (string.IsNullOrEmpty(lineSplit[5]))
                                lineSplit[1] = "S";
                            else
                                lineSplit[1] = (rnd.Next() % 2) == 0 ? "M" : "F";
                        }

                        #region Data di Nascita

                        if (string.IsNullOrEmpty(lineSplit[2]))
                        {
                            indexRnd = rnd.Next(yearList.Count);
                            var year = yearList[indexRnd];
                            var month = rnd.Next(1, 13);
                            var day = 0;
                            if (month == 2)
                                day = rnd.Next(1, 29);
                            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                                day = rnd.Next(1, 32);
                            else
                                day = rnd.Next(1, 31);
                            var dataNascita = new DateTime(year, month, day);
                            lineSplit[2] = dataNascita.ToString();
                        }

                        #endregion

                        #region Stato Civile

                        if (string.IsNullOrEmpty(lineSplit[3]))
                        {
                            indexRnd = rnd.Next() % 3;
                            lineSplit[3] = statoCivileList[indexRnd];
                        }

                        #endregion

                        #region Coniuge a Carico

                        if (string.IsNullOrEmpty(lineSplit[4]))
                        {
                            if (lineSplit[3] == "Coniugato" || lineSplit[4] == "Convivente")
                                lineSplit[4] = (rnd.Next() % 2) == 0 ? System.Boolean.TrueString : System.Boolean.FalseString;
                            else
                                lineSplit[4] = System.Boolean.FalseString;
                        }

                        #endregion

                        #region Numero Figli

                        if (string.IsNullOrEmpty(lineSplit[5]))
                        {
                            if (lineSplit[1] == "0" || lineSplit[1] == "X" || lineSplit[1] == "S")
                                lineSplit[5] = "0";
                            else
                                lineSplit[5] = (rnd.Next() % 4).ToString();
                        }

                        #endregion

                        #region Figli a Carico

                        if (string.IsNullOrEmpty(lineSplit[6]))
                        {
                            var dataNascita = DateTime.Parse(lineSplit[2]);
                            var age = (int)DateTime.Now.Subtract(dataNascita).TotalDays / 365;

                            if (lineSplit[5] == "0")
                                lineSplit[6] = System.Boolean.FalseString;
                            else if (age < 18)
                                lineSplit[6] = System.Boolean.TrueString;
                            else
                                lineSplit[6] = System.Boolean.FalseString;
                        }

                        #endregion

                        #region RAL

                        if (string.IsNullOrEmpty(lineSplit[8]))
                        {
                            var profession = professioneList.FirstOrDefault(x => x[0] == lineSplit[7]);
                            if (profession == null)
                                continue;

                            if ((rnd.Next() % 2) == 0)
                                annualGrossIncomeBase = int.Parse(profession[1]) * (1 + rnd.NextSingle());
                            else
                                annualGrossIncomeBase = int.Parse(profession[2]) * rnd.NextSingle();

                            lineSplit[8] = annualGrossIncomeBase.ToString();
                        }

                        #endregion

                        #region Tipo Reddito

                        if (string.IsNullOrEmpty(lineSplit[9]))
                        {
                            if (lineSplit[1] == "0" || lineSplit[1] == "X" || lineSplit[1] == "S")
                                lineSplit[9] = "Reddito da Libera Professione";
                            else if (liberoProfessionistaList.Any(x => x == lineSplit[7]))
                                lineSplit[9] = "Reddito da Libera Professione";
                            else if (dipedenteList.Any(x => x == lineSplit[7]))
                                lineSplit[9] = "Reddito da Lavoro Dipendente";
                            else if (nullaTenenteList.Any(x => x == lineSplit[7]) && lineSplit[8] == "0")
                                lineSplit[9] = "Nessun Reddito";
                            else if (nullaTenenteList.Any(x => x == lineSplit[7]) && lineSplit[8] != "0")
                                lineSplit[9] = "Altro Reddito";
                            else
                                lineSplit[9] = (rnd.Next() % 2) == 0 ? "Reddito da Lavoro Dipendente" : "Reddito da Libera Professione";
                        }

                        if (!string.IsNullOrEmpty(lineSplit[11]) && lineSplit[11].Length == 2)
                        {
                            if (!provinciaList.Any(x => x == lineSplit[11]))
                                provinciaList.Add(lineSplit[11]);
                        }
                        else if (string.IsNullOrEmpty(lineSplit[11]))
                        {
                            indexRnd = rnd.Next(provinciaList.Count);
                            lineSplit[11] = provinciaList[indexRnd];
                        }

                        #endregion

                        customerList.Add(lineSplit);
                    }
                }

                return customerList;
            }
        }

        public void WriteData(string pathFileCustomer, List<List<string>> customerList)
        {
            customerList[0][0] = "UserId";

            using (var sw = new StreamWriter(pathFileCustomer))
            {
                foreach(var item in customerList)
                {
                    var line = string.Join(";", item);
                    sw.WriteLine(line);
                }
            }    
        }
    }
}
