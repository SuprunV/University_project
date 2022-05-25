using Project_1.Data.Party;

namespace Project_1.Infra.Initializers {
    public sealed class StudyProgramInitializer : BaseInitializer<StudyProgramData> {
        public StudyProgramInitializer(UniversityDb? db) : base(db, db?.StudyPrograms) { }
        protected override IEnumerable<StudyProgramData> getEntities => new[] {
            //Here values add
            createStudyProgram("Economy",4, "description 1"),
            createStudyProgram("Information technology",3, "description 2"),
            createStudyProgram("Science", 6, "description 3"),
            createStudyProgram("Philosophy",2, null)

        };
        internal static StudyProgramData createStudyProgram(string title, int duration, string? descr)
            => new StudyProgramData {
                ID = title.Substring(0,(title.Length <6) ? title.Length : 6),
                StudyProgramsTitle = title,
                Duration = duration,
                StudyProgramsDescription = descr
            };
    }
}
