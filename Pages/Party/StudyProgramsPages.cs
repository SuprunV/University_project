using Project_1.Domain.Party;
using Project_1.Facade.Party;


namespace Project_1.Pages.Party
{
    public class StudyProgramsPages : PagedPage<StudyProgramView, StudyProgram, IStudyProgramsRepo>
    {
        public StudyProgramsPages(IStudyProgramsRepo r) : base(r) { }
        protected override StudyProgram toObject(StudyProgramView? item) => new StudyProgramViewFactory().Create(item);
        protected override StudyProgramView toView(StudyProgram? entity, bool getNullVal = false) => (getNullVal) ? new StudyProgramViewFactory().Create(entity) : new StudyProgramViewFactory().CreateUndefined(entity);
        public override string[] IndexColumns { get; } = new[] {
            nameof(StudyProgramView.StudyProgramsTitle),
            nameof(StudyProgramView.Duration),
            nameof(StudyProgramView.StudyProgramsDescription)
        };
    }
}
