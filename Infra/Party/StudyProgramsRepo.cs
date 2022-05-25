using Project_1.Data.Party;
using Project_1.Domain.Party;


namespace Project_1.Infra.Party
{
    public sealed class StudyProgramsRepo : Repo<StudyProgram, StudyProgramData>, IStudyProgramsRepo
    {
        public StudyProgramsRepo(UniversityDb? db) : base(db, db?.StudyPrograms) { }
        protected internal override StudyProgram toDomain(StudyProgramData d) => new (d);

        internal override IQueryable<StudyProgramData> addFilter(IQueryable<StudyProgramData> q)
        {
            var name = CurrentFilter;
            if (string.IsNullOrWhiteSpace(name)) return q;
            return q.Where(
                x => x.StudyProgramsTitle!.Contains(name)
               || x.Duration.ToString()!.Contains(name));
        } 
    }
}