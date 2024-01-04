namespace DAL.Enums
{
    public enum ContractType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Contratto a Tempo Determinato")]
        Contratto_Tempo_Determinato,
        [ValueInfo(Definition = "Contratto a Tempo Indeterminato")]
        Contratto_Tempo_Indeterminato,
        [ValueInfo(Definition = "Contratto a Partita IVA")]
        Contratto_Partita_Iva,
        [ValueInfo(Definition = "Contratto a Progetto")]
        Contratto_CO_CO_PRO
    }
}