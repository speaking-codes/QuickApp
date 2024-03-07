using System.Threading;

namespace DAL.Enums
{
    public enum EnumMaritalStatus
    {
        [ValueInfo(Definition = "Libero")]
        Libero = 1,
        [ValueInfo(Definition = "Nubile")]
        Nubile = 2,
        [ValueInfo(Definition = "Celibe")]
        Celibe = 3,
        [ValueInfo(Definition = "Coniugato")]
        Coniugato = 4,
        [ValueInfo(Definition = "Vedovo")]
        Vedovo = 5,
        [ValueInfo(Definition = "Separato")]
        Separato = 6,
        [ValueInfo(Definition = "Divorziato")]
        Divorziato = 7,
        [ValueInfo(Definition = "Convivente")]
        Convivente = 8,
    }
}