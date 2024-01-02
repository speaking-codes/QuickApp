namespace Models.Enums
{
    public enum ContractType
    {
        [ValueInfo(Name = "Non Specificato")]
        None,
        [ValueInfo(Name = "Contratto a Tempo Determinato")]
        Contratto_Tempo_Determinato,
        [ValueInfo(Name = "Contratto a Tempo Indeterminato")]
        Contratto_Tempo_Indeterminato,
        [ValueInfo(Name = "Contratto a Partita IVA")]
        Contratto_Partita_Iva,
        [ValueInfo(Name = "Contratto a Progetto")]
        Contratto_CO_CO_PRO
    }
}