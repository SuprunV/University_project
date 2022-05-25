using Project_1.Data.Party;

namespace Project_1.Domain.Party
{
    public sealed class StudyProgram:UniqueEntity<StudyProgramData>
    {
        public StudyProgram() : this(new StudyProgramData()) { }
        public StudyProgram(StudyProgramData d):base(d){}

        public string? StudyProgramsTitle => getValue(Data?.StudyProgramsTitle);
        public int? Duration => getValue(Data?.Duration);
        public string? StudyProgramsDescription =>getValue(Data?.StudyProgramsDescription);


    }
    public interface IStudyProgramsRepo : IRepo<StudyProgram>
    { }
}
