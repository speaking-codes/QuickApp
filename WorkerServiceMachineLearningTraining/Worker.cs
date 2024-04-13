using Amazon.Util.Internal;
using DAL;
using DAL.Core.Interfaces;
using DAL.Models;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkerServiceMachineLearningTraining
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILearningManager _learningManager;

        private IList<string> getCopertureString(IList<string> coperture)
        {
            var copertureItem = new List<List<string>>();
            var innerList=new List<string>();

            if (coperture.Count == 2)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);
            }
            else if (coperture.Count == 3)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);

                innerList=new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);
            }
            else if (coperture.Count == 4)
            {
                innerList.Add(coperture[0]);
                innerList.Add(coperture[1]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[0]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[2]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[1]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);

                innerList = new List<string>();
                innerList.Add(coperture[3]);
                innerList.Add(coperture[3]);
                copertureItem.Add(innerList);
            }

            var copertureItemReverse=new List<List<string>>();
            for (var i=0;i<copertureItem.Count;i++)
            {
                copertureItem[i].Reverse();
                copertureItemReverse.Add(copertureItem[i]);
                copertureItem[i].Reverse();
            }

            var copertureItemEquals =new List<List<string>>();
            foreach(var item in coperture)
            {
                innerList = new List<string>();
                innerList.Add(item);
                innerList.Add(item);
                copertureItemEquals.Add(innerList);
            }

            var copertureTotal = new List<List<string>>();
            copertureTotal.AddRange(copertureItem);
            copertureTotal.AddRange(copertureItemReverse);
            copertureTotal.AddRange(copertureItemEquals);

            var copertureOutput=new List<string>();

            foreach (var item in copertureTotal)
                copertureOutput.Add(string.Join(";", item));

            return copertureOutput;
        }

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork, ILearningManager learningManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _learningManager = learningManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var rnd = new Random();
                    var indexRnd = 0;
                    var professioneList = new List<List<string>>();
                    var customerList = new List<List<string>>();
                    var lineSplit = new List<string>();
                    float annualGrossIncomeBase = 0f;

                    var yearList = new List<int>();
                    for (var year = 1915; year <= 2006; year++)
                        yearList.Add(year);

                    var fascieReddito = new List<FasciaReddito>() {
                                new FasciaReddito{ RalMinima=0, RalMassima =15000, Descrizione = "reddito basso" },
                                new FasciaReddito{ RalMinima=15001, RalMassima =28000, Descrizione = "reddito medio-basso" },
                                new FasciaReddito{ RalMinima=28001, RalMassima =55000, Descrizione = "reddito medio" },
                                new FasciaReddito{ RalMinima=55001, RalMassima =75000, Descrizione = "reddito medio-alto" },
                                new FasciaReddito{ RalMinima=75001, RalMassima =120000, Descrizione = "reddito alto" },
                                new FasciaReddito{ RalMinima=120000, RalMassima =null, Descrizione = "reddito molto alto"}
                        };
                    var statoCivileList = new List<string>()
                    {
                        "Divorziato",
                        "Convivente",
                        "Vedovo"
                    };
                    var statoCivileSingleList = new List<string>()
                    {
                        "Celibe/nubile",
                        "Divorziato",
                        "Vedovo"
                    };
                    var statoCivileCoppiaList = new List<string>()
                    {
                        "Coniugato",
                        "Convivente",
                    };
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

                    var pathDirectory = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ExportPolizzeMauroDiLiddo\";
                    var pathFileProfessioni = $"{pathDirectory}Professioni - Copy.txt";
                    var pathFileCustomer = $"{pathDirectory}ExportPolizzeMauroDiLiddo.csv";
                    var pathFileCustomerToWrite = $"{pathDirectory}ExportPolizzeMauroDiLiddo_V1.csv";
                    var pathFileCustomerFinal = $"{pathDirectory}ExportPolizzeMauroDiLiddo_Finale.csv";

                    var pathFileGroup = $"{pathDirectory}CustomerReports.txt";

                    var provinciaList = new List<string>();

                    using (var sr = new StreamReader(pathFileProfessioni))
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                lineSplit = line.Split(new char[] { ';' }).ToList();
                                professioneList.Add(lineSplit);
                            }
                        }
                    }

                    using (var sr = new StreamReader(pathFileCustomer))
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                lineSplit = line.Split(new char[] { ';' }).ToList();

                                if (string.IsNullOrEmpty(lineSplit[0].Trim()))
                                {
                                    if (string.IsNullOrEmpty(lineSplit[4]))
                                        lineSplit[0] = "S";
                                    else
                                        lineSplit[0] = (rnd.Next() % 2) == 0 ? "M" : "F";
                                }

                                #region Data di Nascita

                                if (string.IsNullOrEmpty(lineSplit[1]))
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
                                    lineSplit[1] = dataNascita.ToString();
                                }

                                #endregion

                                #region Stato Civile

                                if (string.IsNullOrEmpty(lineSplit[2]))
                                {
                                    indexRnd = rnd.Next() % 3;
                                    lineSplit[2] = statoCivileList[indexRnd];
                                }

                                #endregion

                                #region Coniuge a Carico

                                if (string.IsNullOrEmpty(lineSplit[3]))
                                {
                                    if (lineSplit[2] == "Coniugato" || lineSplit[2] == "Convivente")
                                        lineSplit[3] = (rnd.Next() % 2) == 0 ? System.Boolean.TrueString : System.Boolean.FalseString;
                                    else
                                        lineSplit[3] = System.Boolean.FalseString;
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

                                #region Figli a Carico

                                if (string.IsNullOrEmpty(lineSplit[5]))
                                {
                                    var dataNascita = DateTime.Parse(lineSplit[1]);
                                    var age = (int)DateTime.Now.Subtract(dataNascita).TotalDays / 365;

                                    if (lineSplit[4] == "0")
                                        lineSplit[5] = System.Boolean.FalseString;
                                    else if (age < 18)
                                        lineSplit[5] = System.Boolean.TrueString;
                                    else
                                        lineSplit[5] = System.Boolean.FalseString;
                                }

                                #endregion

                                #region RAL

                                if (string.IsNullOrEmpty(lineSplit[7]))
                                {
                                    var profession = professioneList.FirstOrDefault(x => x[0] == lineSplit[6]);
                                    if (profession == null)
                                        continue;

                                    if ((rnd.Next() % 2) == 0)
                                        annualGrossIncomeBase = int.Parse(profession[1]) * (1 + rnd.NextSingle());
                                    else
                                        annualGrossIncomeBase = int.Parse(profession[2]) * rnd.NextSingle();

                                    lineSplit[7] = annualGrossIncomeBase.ToString();
                                }

                                #endregion

                                #region Tipo Reddito

                                if (string.IsNullOrEmpty(lineSplit[8]))
                                {
                                    if (lineSplit[0] == "0" || lineSplit[0] == "X" || lineSplit[0] == "S")
                                        lineSplit[8] = "Reddito da Libera Professione";
                                    else if (liberoProfessionistaList.Any(x => x == lineSplit[6]))
                                        lineSplit[8] = "Reddito da Libera Professione";
                                    else if (dipedenteList.Any(x => x == lineSplit[6]))
                                        lineSplit[8] = "Reddito da Lavoro Dipendente";
                                    else if (nullaTenenteList.Any(x => x == lineSplit[6]) && lineSplit[7] == "0")
                                        lineSplit[8] = "Nessun Reddito";
                                    else if (nullaTenenteList.Any(x => x == lineSplit[6]) && lineSplit[7] != "0")
                                        lineSplit[8] = "Altro Reddito";
                                    else
                                        lineSplit[8] = (rnd.Next() % 2) == 0 ? "Reddito da Lavoro Dipendente" : "Reddito da Libera Professione";
                                }

                                if (!string.IsNullOrEmpty(lineSplit[10]) && lineSplit[10].Length == 2)
                                {
                                    if (!provinciaList.Any(x => x == lineSplit[10]))
                                        provinciaList.Add(lineSplit[10]);
                                }
                                else if (string.IsNullOrEmpty(lineSplit[10]))
                                {
                                    indexRnd = rnd.Next(provinciaList.Count);
                                    lineSplit[10] = provinciaList[indexRnd];
                                }

                                #endregion

                                customerList.Add(lineSplit);
                            }
                        }
                    }
                    var temp = customerList.Where(x => string.IsNullOrEmpty(x[10])).ToList();
                    using (var sw = new StreamWriter(pathFileCustomerToWrite))
                    {
                        foreach (var item in customerList)
                            sw.WriteLine(string.Join(";", item));
                    }
                    var uominiGroupList = customerList.Where(x => x[0] == "M").GroupBy(x => x[11]).Select(y => new { itemKey = y.Key, itemCount = y.Count() }).ToList();
                    var donneGroupList = customerList.Where(x => x[0] == "F").GroupBy(x => x[11]).Select(y => new { itemKey = y.Key, itemCount = y.Count() }).ToList();
                    var personeFisicheGroupList = customerList.Where(x => x[0] == "M" || x[0] == "F").GroupBy(x => x[11]).Select(y => new { itemKey = y.Key, itemCount = y.Count() }).ToList();
                    var societaGroupList = customerList.Where(x => x[0] != "M" && x[0] != "F").GroupBy(x => x[11]).Select(y => new { itemKey = y.Key, itemCount = y.Count() }).ToList();
                    var groupList = customerList.GroupBy(x => x[11]).Select(y => new { itemKey = y.Key, itemCount = y.Count() }).ToList();
                    using (var sw = new StreamWriter(pathFileGroup))
                    {
                        sw.WriteLine("Generale");
                        foreach (var item in groupList)
                            sw.WriteLine($"{item.itemKey} - {item.itemCount}");
                        sw.WriteLine();

                        sw.WriteLine("Uomini");
                        foreach (var item in uominiGroupList)
                            sw.WriteLine($"{item.itemKey} - {item.itemCount}");
                        sw.WriteLine();

                        sw.WriteLine("Donne");
                        foreach (var item in donneGroupList)
                            sw.WriteLine($"{item.itemKey} - {item.itemCount}");
                        sw.WriteLine();

                        sw.WriteLine("Persone Fisiche");
                        foreach (var item in personeFisicheGroupList)
                            sw.WriteLine($"{item.itemKey} - {item.itemCount}");
                        sw.WriteLine();

                        sw.WriteLine("Persone Giuridiche");
                        foreach (var item in societaGroupList)
                            sw.WriteLine($"{item.itemKey} - {item.itemCount}");
                        sw.WriteLine();
                    }

                    var header = "Sesso;AnnoNascita;ComposizioneNucleoFamiliare;ConiugeCarico;Professione;FasciaReddito;TipoReddito;Residenza;PolizzaStipulata;PolizzaTarget";
                    customerList.RemoveAt(0);
                    var clienteList = customerList.Select(x => new Cliente
                    {
                        Sesso = x[0],
                        DataNascita = x[1],
                        StatoCivile = x[2],
                        ConiugeCarico = x[3],
                        NumeroFigli = x[4],
                        NumeroFigliCarico = x[5],
                        Professione = x[6],
                        Reddito = x[7],
                        TipoImpiego = x[8],
                        TipologiaContratto = x[9],
                        Residenza = x[10],
                        TipoligiaPolizzaStipulate = x[11],
                    })
                    .ToList();


                    var clienteStepOneList = clienteList.Select(x => x.ToClienteStepOne()).ToList();
                    var clienteStepTwoList = clienteStepOneList.Select(x => x.ToClienteStepTwo(statoCivileSingleList, statoCivileCoppiaList, fascieReddito)).ToList();
                    var clienteStepThreeList = clienteStepTwoList.GroupBy(cliente => new
                    {
                        cliente.Sesso,
                        cliente.AnnoNascita,
                        cliente.ComposizioneNucleo,
                        cliente.ConiugeCarico,
                        cliente.Professione,
                        cliente.FasciaReddito,
                        cliente.TipoImpiego,
                        cliente.Residenza
                    })
                    .Select(group => new
                    {
                        GroupKey = group.Key,
                        Coperture = group.Select(x => x.TipoligiaPolizzaStipulata.Trim()).Distinct().ToList()
                    })
                    .ToList();

                    var clienteStepFinalOneList = clienteStepThreeList.Where(x => x.Coperture.Count == 1).ToList();
                    var clienteStepFinalTwoList = clienteStepThreeList.Where(x => x.Coperture.Count > 1).ToList();

                    using (var sw = new StreamWriter(pathFileCustomerFinal))
                    {
                        sw.WriteLine(header);

                        foreach (var item in clienteStepFinalOneList)
                        {
                            var lista = new List<string>();
                            lista.Add(item.GroupKey.Sesso);
                            lista.Add(item.GroupKey.AnnoNascita.ToString());
                            lista.Add(item.GroupKey.ComposizioneNucleo);
                            lista.Add(item.GroupKey.ConiugeCarico.ToString());
                            lista.Add(item.GroupKey.Professione);
                            lista.Add(item.GroupKey.FasciaReddito);
                            lista.Add(item.GroupKey.TipoImpiego);
                            lista.Add(item.GroupKey.Residenza);
                            lista.Add(item.Coperture[0]);

                            var line = string.Join(";", lista);
                            sw.WriteLine(line);
                        }

                        foreach (var item in clienteStepFinalTwoList)
                        {
                            var lista = new List<string>();
                            lista.Add(item.GroupKey.Sesso);
                            lista.Add(item.GroupKey.AnnoNascita.ToString());
                            lista.Add(item.GroupKey.ComposizioneNucleo);
                            lista.Add(item.GroupKey.ConiugeCarico.ToString());
                            lista.Add(item.GroupKey.Professione);
                            lista.Add(item.GroupKey.FasciaReddito);
                            lista.Add(item.GroupKey.TipoImpiego);
                            lista.Add(item.GroupKey.Residenza);

                            var copertureString = getCopertureString(item.Coperture);

                            foreach (var coperturaItem in copertureString)
                            {
                                var sbLine = new StringBuilder(string.Empty);
                                sbLine.Append(string.Join(";", lista));
                                sbLine.Append($";{coperturaItem}");
                                sw.WriteLine(sbLine.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error Message: {ex.Message}");
                    _logger.LogError($"Error StackTrace: {ex.StackTrace}");
                    _logger.LogError($"Error Source: {ex.Source}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }

    class Cliente
    {
        public string Sesso { get; set; }
        public string DataNascita { get; set; }
        public string StatoCivile { get; set; }
        public string ConiugeCarico { get; set; }
        public string NumeroFigli { get; set; }
        public string NumeroFigliCarico { get; set; }
        public string Professione { get; set; }
        public string Reddito { get; set; }
        public string TipoImpiego { get; set; }
        public string TipologiaContratto { get; set; }
        public string Residenza { get; set; }
        public string TipoligiaPolizzaStipulate { get; set; }
    }

    class ClienteStepOne
    {
        public string Sesso { get; set; }
        public int AnnoNascita { get; set; }
        public string StatoCivile { get; set; }
        public bool ConiugeCarico { get; set; }
        public int NumeroFigli { get; set; }
        public bool FigliCarico { get; set; }
        public string Professione { get; set; }
        public double Reddito { get; set; }
        public string TipoImpiego { get; set; }
        public string Residenza { get; set; }
        public string TipoligiaPolizzaStipulata { get; set; }
    }

    class ClienteStepTwo
    {
        public string Sesso { get; set; }
        public int AnnoNascita { get; set; }
        public string ComposizioneNucleo { get; set; }
        public bool ConiugeCarico { get; set; }
        public string Professione { get; set; }
        public string FasciaReddito { get; set; }
        public string TipoImpiego { get; set; }
        public string Residenza { get; set; }
        public string TipoligiaPolizzaStipulata { get; set; }
    }

    //class ClienteStepFinal
    //{
    //    public string Sesso { get; set; }
    //    public int AnnoNascita { get; set; }
    //    public string ComposizioneNucleo { get; set; }
    //    public bool ConiugeCarico { get; set; }
    //    public string Professione { get; set; }
    //    public string FasciaReddito { get; set; }
    //    public string TipoImpiego { get; set; }
    //    public string Residenza { get; set; }
    //    public IList<CoperturaStipulata> CoperturaStipulate { get; set; }
    //}

    class CoperturaStipulata
    {
        public string Tipologia { get; set; }
        public int NumeroCopertura { get; set; }
    }

    class FasciaReddito
    {
        public double RalMinima { get; set; }
        public double? RalMassima { get; set; }
        public string Descrizione { get; set; }
    }

    static class MappingEntities
    {
        public static ClienteStepOne ToClienteStepOne(this Cliente cliente)
        {
            return new ClienteStepOne()
            {
                Sesso = cliente.Sesso,
                AnnoNascita = DateTime.Parse(cliente.DataNascita).Year,
                StatoCivile = cliente.StatoCivile,
                ConiugeCarico = bool.Parse(cliente.ConiugeCarico),
                NumeroFigli = int.Parse(cliente.NumeroFigli),
                FigliCarico = bool.Parse(cliente.NumeroFigliCarico),
                Professione = cliente.Professione,
                Reddito = double.Parse(cliente.Reddito),
                TipoImpiego = cliente.TipoImpiego,
                Residenza = cliente.Residenza,
                TipoligiaPolizzaStipulata = cliente.TipoligiaPolizzaStipulate,
            };
        }

        public static ClienteStepTwo ToClienteStepTwo(this ClienteStepOne cliente, IEnumerable<string> statiCivileSingle, IEnumerable<string> statiCivileCoppia, IEnumerable<FasciaReddito> fasceReddito)
        {
            var output = new ClienteStepTwo()
            {
                Sesso = cliente.Sesso,
                AnnoNascita = cliente.AnnoNascita,
                ConiugeCarico = cliente.ConiugeCarico,
                Professione = cliente.Professione,
                TipoImpiego = cliente.TipoImpiego,
                Residenza = cliente.Residenza,
                TipoligiaPolizzaStipulata = cliente.TipoligiaPolizzaStipulata,
            };

            var composizioneNucleo = string.Empty;
            if (statiCivileSingle.Any(x => x == cliente.StatoCivile))
                composizioneNucleo = "Single ";
            if (statiCivileCoppia.Any(x => x == cliente.StatoCivile))
                composizioneNucleo = "Coppia ";

            if (cliente.NumeroFigli == 0)
                composizioneNucleo += "senza figli";
            else if (cliente.NumeroFigli == 1)
                composizioneNucleo += "con un figlio";
            else if (cliente.NumeroFigli > 1)
                composizioneNucleo += "con figli";

            if (cliente.NumeroFigli > 0 && cliente.FigliCarico)
                composizioneNucleo += " a carico";

            var fasciaReddito = fasceReddito.FirstOrDefault(x => cliente.Reddito >= x.RalMinima && (!x.RalMassima.HasValue || cliente.Reddito <= x.RalMassima.Value))?.Descrizione;

            output.ComposizioneNucleo = composizioneNucleo;
            output.FasciaReddito = fasciaReddito;

            return output;
        }
    }
}