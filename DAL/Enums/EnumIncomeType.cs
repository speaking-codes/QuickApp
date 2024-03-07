namespace DAL.Enums
{
    public enum EnumIncomeType
    {
        [ValueInfo(Definition ="Dipendente")]
        Dipendente = 1,
        [ValueInfo(Definition = "Autonomo")]
        Autonomo = 2,
        [ValueInfo(Definition = "Professionista")]
        Professionista = 3
    }
}