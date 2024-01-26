using System.Diagnostics.Contracts;

namespace DAL.Enums
{
    public enum EnumContractType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Contratto a Termine - (CDD)")]
        Contratto_A_Termine,
        [ValueInfo(Definition = "Contratto a Tempo Determinato - (CDI)")]
        Contratto_Tempo_Indeterminato,
        Contratto_Apprendistato,
        [ValueInfo(Definition = "Partita IVA")]
        Partita_Iva,
        [ValueInfo(Definition = "Contratto a Progetto - (Co.Co.Pro.)")]
        Contratto_CO_CO_PRO,
        [ValueInfo(Definition = "Contratto di collaborazione coordinata e continuativa - (Co.Co.Co.)")]
        Contratto_CO_CO_CO,
        [ValueInfo(Definition = "Contratto di somministrazione")]
        Contratto_Somministrazione,
        [ValueInfo(Definition = "Contratto di lavoro Intermittente")]
        Contratto_Lavoro_Intermittente,
        [ValueInfo(Definition = "Contratto di lavoro Part Time")]
        Contratto_Lavoro_Part_Time,
        [ValueInfo(Definition = "Contratto di lavoro a Domicilio")]
        Contratto_Lavoro_Domicilio
    }
}