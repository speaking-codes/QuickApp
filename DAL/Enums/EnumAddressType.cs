namespace DAL.Enums
{
    public enum EnumAddressType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Residenza")]
        Residenza,
        [ValueInfo(Definition = "Lavoro")]
        Lavoro,
        [ValueInfo(Definition = "Domicilio")]
        Domicilio
    }
}
