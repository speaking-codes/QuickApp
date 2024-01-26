using System.Threading;

namespace DAL.Enums
{
    public enum EnumMaritalStatus
    {
        [ValueInfo(Definition ="Celibe/Nubile")]
        Celibate,
        [ValueInfo(Definition = "Coniugato/a")]
        Married,
        [ValueInfo(Definition = "Separato/a")]
        Separate,
        [ValueInfo(Definition = "Divorziato/a")]
        Divorced,
        [ValueInfo(Definition = "Vedovo/a")]
        Widower
    }
}