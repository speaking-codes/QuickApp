namespace Models.Enums
{
    public enum ContractType
    {
        [ValueInfo(Name = "Contratto a Tempo Determinato")]
        FixedTerm,
        [ValueInfo(Name = "Contratto a Tempo Indeterminato")]
        IndefinitePeriod,
        [ValueInfo(Name = "Contratto a Partita IVA")]
        VATNumber,
        [ValueInfo(Name = "Contratto a Progetto")]
        ToProject
    }
}