namespace DAL.Enums
{
    public enum EnumLuggageType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Zaino da Viaggio")]
        TravleBackpack,
        [ValueInfo(Definition = "Zaino da Campeggio")]
        CampingBackpack,
        [ValueInfo(Definition = "Valigia")]
        Suitcase,
        [ValueInfo(Definition = "Trolley")] 
        Trolley
    }
}
