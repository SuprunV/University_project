using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Domain.Party
{ 
    public sealed class Semester:UniqueEntity<SemesterData>
    {
        public Semester() : this(new SemesterData()) { }
        public Semester(SemesterData d) : base(d) { }
        public DateTime DateStart => getValue(Data?.DateStart);
        public DateTime DateEnd => getValue(Data?.DateEnd);
        public IsoSemester Season => Data?.Season ?? IsoSemester.NotApplicable;
        public string? SeasonFull() => $"{Season} {DateStart.Year}";
    }
    public interface ISemestersRepo : IRepo<Semester> { }

}
