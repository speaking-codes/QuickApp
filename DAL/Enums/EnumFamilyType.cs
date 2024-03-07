namespace DAL.Enums
{
    public enum EnumFamilyType
    {
        [ValueInfo(Definition ="Single senza figli")]
        Single_Senza_Figli = 1,
        [ValueInfo(Definition ="Single con figli")]
        Single_Con_Figli = 2,
        [ValueInfo(Definition ="Coppia senza figli")]
        Coppia_Senza_Figli = 3,
        [ValueInfo(Definition ="Coppia con un figlio")]
        Coppia_Con_1_Figlio = 4,
        [ValueInfo(Definition ="Coppia con 2 figli")]
        Coppia_Con_2_Figli = 5,
        [ValueInfo(Definition ="Coppia con più di 2 figli")]
        Coppia_Con_3_Figli = 6,
        [ValueInfo(Definition ="Nucleo con familiari a carico")]
        Nucleo_Con_Familiari_A_Carico = 7
    }
}