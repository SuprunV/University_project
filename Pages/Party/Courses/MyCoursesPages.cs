using Microsoft.AspNetCore.Mvc;
using Project_1.Domain.Connection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;

namespace Project_1.Pages.Party.Courses {
    public class MyCoursesPages : PagedPage<CourseView, Course, IMyCoursesRepo>
    {
        public ISemesterCourseRepo semesterCourseRepo;
        public IStudyProgramsCoursesRepo studyProgramCoursesRepo;
        public List<StudyProgramCourse> studyProgramCoursesList;
        public List<SemesterCourse> semesterCoursesList;
        public List<Lecturer> lecturersList;
        public List<Semester> semestersList;
        public List<StudyProgram> studyProgramsList;
        public override Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }
        public MyCoursesPages(IMyCoursesRepo r, ISemesterCourseRepo semCou, IStudyProgramsCoursesRepo studyPrCou, ISemestersRepo s, ILecturersRepo l, IStudyProgramsRepo sp) :
            base(r)
        {
            semesterCourseRepo = semCou;
            studyProgramCoursesRepo = studyPrCou;
            studyProgramCoursesList = studyProgramCoursesRepo.GetAll(x => x.CourseID ?? String.Empty);
            semesterCoursesList = semesterCourseRepo.GetAll(x => x.CourseID);


            studyProgramsList = sp.GetAll(x => x.StudyProgramsTitle ?? String.Empty);
            lecturersList = l.GetAll(x => x.FullName ?? String.Empty);
            semestersList = s.GetAll(x => x.ID);

            SelectFilters = new()
            {
                { nameof(CourseView.StudyProgramID), studyProgramsList.Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID)) },
                { nameof(CourseView.SemesterID), semestersList.Select(x => new SelectListItem(x.SeasonFull(), x.ID)) },
            };
        }

        protected override Course toObject(CourseView? item) => new CourseViewFactory().Create(item);

        protected override CourseView toView(Course? entity, bool getNullVal = false) => (getNullVal) ? new CourseViewFactory().Create(entity) : new CourseViewFactory().CreateUndefined(entity);
    
    

        public override string[] IndexColumns { get; } = new[] {
            nameof(CourseView.CourseTitle),
            nameof(CourseView.EAP),
            nameof(CourseView.StudyProgramID),
            nameof(CourseView.SemesterID),
            nameof(CourseView.OwnerID),
            nameof(CourseView.Description)
        };
        public IEnumerable<SelectListItem> Semesters
            => semestersList.Select(x => new SelectListItem(x.SeasonFull(), x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Lecturer
            => lecturersList.Select(x => new SelectListItem(x.FullName, x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> StudyPrograms
            => studyProgramsList.Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID))
               ?? new List<SelectListItem>();
        public string SemesterName(string? semID = null)
            => Semesters?.FirstOrDefault(x => x.Value == (semID ?? string.Empty))?.Text ?? "Unspecified";
        public string LecturerName(string? lecID = null)
            => Lecturer?.FirstOrDefault(x => x.Value == (lecID ?? string.Empty))?.Text ?? "Unspecified";
        public string StudyProgramName(string? spID = null)
            => StudyPrograms?.FirstOrDefault(x => x.Value == (spID ?? string.Empty))?.Text ?? "Unspecified";
        public override object? GetValue(string name, CourseView v)
        {
            var r = base.GetValue(name, v);
            switch (name)
            {
                case nameof(CourseView.SemesterID): return SemesterName(r as string);
                case nameof(CourseView.StudyProgramID): return StudyProgramName(r as string);
                case nameof(CourseView.OwnerID): return LecturerName(r as string);
                default: return r;
            }
        }
    }
}
