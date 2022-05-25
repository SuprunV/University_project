using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;
using Project_1.Infra.Party.Courses;

namespace Project_1.Pages.Party.Courses {
    public class JoinedCoursesPages : PagedPage<CourseView, Course, IJoinedCoursesRepo> {
        public List<Lecturer> lecturersList;
        public List<Semester> semestersList;
        public List<StudyProgram> studyProgramsList;
        public List<CourseLecturer> courseLecturersList;
        public ICourseLecturerRepo courseLecturerRepo;

        public override Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }
        public JoinedCoursesPages(IJoinedCoursesRepo r, ISemestersRepo s, ILecturersRepo l, IStudyProgramsRepo sp, ICourseLecturerRepo cl) : base(r)
        {
            courseLecturerRepo = cl;
            studyProgramsList = sp.GetAll(x => x.StudyProgramsTitle ?? String.Empty);
            lecturersList = l.GetAll(x => x.FullName ?? String.Empty);
            semestersList = s.GetAll(x => x.ID);
            courseLecturersList = cl.GetAll(x => x.ID);
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

        public override string? additionalControlString(string courseID) {
            return ControllJoined.isJoinedToCourse(courseID, courseLecturersList, SessionUserID) ? "Unjoin" : "Join";
        }

        public async Task<IActionResult> OnGetJoinAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null) {

            CourseLecturerView cl = new CourseLecturerView() {
                CourseID = id,
                LecturerID = SessionUserID ?? String.Empty
            };

            CourseLecturer clObj = new CourseLecturerViewFactory().Create(cl);

            var allIDcou = courseLecturerRepo.GetAll(x => x.ID).Where(elem => (elem.CourseID == cl.CourseID) && (elem.LecturerID == cl.LecturerID)).FirstOrDefault();

            if (allIDcou == null) await courseLecturerRepo.AddAsync(clObj);
            else await courseLecturerRepo.DeleteAsync(allIDcou.ID);

            setAttributes(pageIndex, currentFilter, sortOrder, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage);

            return redirectToIndex(toNativePage: false);
        }

    }
}
