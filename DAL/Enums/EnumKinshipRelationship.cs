namespace DAL.Enums
{
    public enum EnumKinshipRelationship
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Genitore")]
        Parent,
        [ValueInfo(Definition = "Coniuge")]
        Spouse,
        [ValueInfo(Definition = "Figlio")]
        Son,
        [ValueInfo(Definition = "Altro Familiare")]
        Other_Family_Member
    }
}
