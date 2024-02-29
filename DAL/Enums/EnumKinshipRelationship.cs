namespace DAL.Enums
{
    public enum EnumKinshipRelationship
    {
        [ValueInfo(Definition = "Non Specificato")]
        None=0,
        [ValueInfo(Definition = "Genitore")]
        Parent=1,
        [ValueInfo(Definition = "Coniuge")]
        Spouse=2,
        [ValueInfo(Definition = "Figlio/a")]
        Son=3,
        [ValueInfo(Definition ="Fratello/Sorella")]
        Brother=4,
        [ValueInfo(Definition = "Altro Familiare")]
        Other_Family_Member=5
    }
}
