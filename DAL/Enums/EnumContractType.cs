using System.Diagnostics.Contracts;

namespace DAL.Enums
{
    public enum EnumContractType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None = 0,
        [ValueInfo(Definition = "Contratto a Termine - (CDD)")]
        Contratto_A_Termine = 1,
        [ValueInfo(Definition = "Contratto a Tempo Determinato - (CDI)")]
        Contratto_Tempo_Indeterminato = 2,
        Contratto_Apprendistato = 3,
        [ValueInfo(Definition = "Partita IVA")]
        Partita_Iva = 4,
        [ValueInfo(Definition = "Contratto a Progetto - (Co.Co.Pro.)")]
        Contratto_CO_CO_PRO = 5,
        [ValueInfo(Definition = "Contratto di collaborazione coordinata e continuativa - (Co.Co.Co.)")]
        Contratto_CO_CO_CO = 6,
        [ValueInfo(Definition = "Contratto di somministrazione")]
        Contratto_Somministrazione = 7,
        [ValueInfo(Definition = "Contratto di lavoro Intermittente")]
        Contratto_Lavoro_Intermittente = 8,
        [ValueInfo(Definition = "Contratto di lavoro Part Time")]
        Contratto_Lavoro_Part_Time = 9,
        [ValueInfo(Definition = "Contratto di lavoro a Domicilio")]
        Contratto_Lavoro_Domicilio = 10
    }
}