namespace DAL.Enums
{
    public enum EnumStructureType
    {
        [ValueInfo(Definition = "Non Specificato")]
        None,
        [ValueInfo(Definition ="Albergo")]
        Hotel,
        [ValueInfo(Definition ="Bed and Breakfast")]
        BB,
        [ValueInfo(Definition ="Ostello")]
        Hostel,
        [ValueInfo(Definition ="Campeggio")]
        Campsites,
        [ValueInfo(Definition ="Casa Vacanze")]
        HolidayHomes,
        [ValueInfo(Definition ="Albergo di Lusso")]
        LuxuryHotel,
        [ValueInfo(Definition ="Resorts")]
        Resorts
    }
}
