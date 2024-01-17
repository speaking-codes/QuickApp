namespace DAL.Enums
{
    public enum EnumMeansTransportType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition = "Treno")]
        Train,
        [ValueInfo(Definition = "Autobus")]
        Bus,
        [ValueInfo(Definition = "Automobile")]
        Car,
        [ValueInfo(Definition = "Aereo")]
        Airplane
    }
}
