using Project_1.Data.IsoEnums;

namespace Project_1.Data.Party
{
    public sealed class SemesterData:UniqueData
    {
        public IsoSemester? Season { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
       
    }
}
