using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;

namespace Project_1.Pages.Connection
{
    public class StudyProgramsCoursesPages : PagedPage<StudyProgramCourseView, StudyProgramCourse, IStudyProgramsCoursesRepo>
    {
        public readonly ICoursesRepo courses;
        public readonly IStudyProgramsRepo studyProgram;
        
        public StudyProgramsCoursesPages(IStudyProgramsCoursesRepo r, IStudyProgramsRepo s, ICoursesRepo c) : base(r)
        {
            courses = c;
            studyProgram = s;
        }

        protected override StudyProgramCourseView toView(StudyProgramCourse? entity, bool getNullVal = false) => new StudyProgramCourseViewFactory().Create(entity);
        protected override StudyProgramCourse toObject(StudyProgramCourseView? item) => new StudyProgramCourseViewFactory().Create(item);
        public override string[] IndexColumns { get; } = new[] {
            nameof(StudyProgramCourseView.StudyProgramID),
            nameof(StudyProgramCourseView.CourseID),
        };
        public IEnumerable<SelectListItem> StudyProgram
            => studyProgram?.GetAll(x => x?.StudyProgramsTitle ?? String.Empty)
                   .Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Courses
            => courses?.GetAll(x => x?.CreateTotalString() ?? String.Empty)
                   .Select(x => new SelectListItem(x.CreateTotalString(), x.ID))
               ?? new List<SelectListItem>();
       
        public string StudyProgramName(string? Id = null)
            => StudyProgram?.FirstOrDefault(x => x.Value == (Id ?? string.Empty))?.Text ?? "Unspecified";
        public string CourseName(string? Id = null)
            => Courses?.FirstOrDefault(x => x.Value == (Id ?? string.Empty))?.Text ?? "Unspecified";
       
        public override object? GetValue(string name, StudyProgramCourseView v)
        {
            var r = base.GetValue(name, v);
            return name == nameof(StudyProgramCourseView.StudyProgramID) ? StudyProgramName(r as string)
                : name == nameof(StudyProgramCourseView.CourseID) ? CourseName(r as string)
                : r;
        }

    }
  
}
