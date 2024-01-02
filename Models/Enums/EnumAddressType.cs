namespace Models.Enums
{
    public enum EnumAddressType
    {
        [ValueInfo(Name = "Non Specificato")]
        None,
        [ValueInfo(Name = "Residenza")]
        Residenza,
        [ValueInfo(Name = "Lavoro")]
        Lavoro,
        [ValueInfo(Name = "Domicilio")]
        Domicilio
    }
}
