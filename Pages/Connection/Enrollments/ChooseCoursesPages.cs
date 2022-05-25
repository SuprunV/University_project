
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;
using Project_1.Pages.Party.Courses;

namespace Project_1.Pages.Connection.Enrollments {
    public class ChooseCoursesPages : CoursesPages
    {
        private readonly ICoursesRepo? coursesRepo;
        private readonly ILecturersRepo? lecturersRepo;
        private readonly ICourseLecturerRepo? courseLecturerR;
        private readonly ISemesterCourseRepo? semesterCourseR;
        private readonly ISemestersRepo? semestersRepo;
        private readonly IEnrollmentsRepo? enrollmentsRepo;
        private readonly List<Enrollment> enrollments;
        private readonly List<Student> students;
        private readonly List<StudyProgram> studyPrograms;
        private readonly List<SemesterCourse> semesterCourses;
        private List<string>? courseLecturersId;
        public override Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }
        public string mandatoryStudyProgram { get {
                string mandatoryStudyProgramID = students?.FirstOrDefault(x => x.ID == SessionUserID)?.StudyProgramID ?? string.Empty;
                return mandatoryStudyProgram = studyPrograms?.FirstOrDefault(x => x.ID == mandatoryStudyProgramID)?.StudyProgramsTitle ?? String.Empty;
            } set { } }
        public override List<string> CheckBoxFilters { get; }
        public bool StudentIsInCourse { get; set; }
        public IEnumerable<SelectListItem> Lecturers { get{
            var clo = courseLecturerR?.GetAll(x => x.ID);
            var clw = clo?.Where(x => x.CourseID == Item.ID).ToList();
            courseLecturersId = clw?.Select(x => x.LecturerID).ToList() ?? new List<string>(); 
            return lecturersRepo?.GetAll(x => x.LastName ?? String.Empty)
                       .Where(x => courseLecturersId.Contains(x.ID))
                       .Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.ID))
                   ?? new List<SelectListItem>();
        }  set{} }
        public ChooseCoursesPages(ICoursesRepo r, ISemestersRepo s, ISemesterCourseRepo sC, ILecturersRepo l,ICourseLecturerRepo cL, IEnrollmentsRepo e, IStudyProgramsRepo sp, IStudyProgramsCoursesRepo spcr, IStudentsRepo st) : base(r, cL, l, s, sC, sp, spcr) {
            coursesRepo = r;
            enrollmentsRepo = e;
            semesterCourseR = sC;
            semestersRepo = s;
            lecturersRepo = l;
            courseLecturerR = cL;
            enrollments = e.GetAll(x => x.ID).ToList();
            semesterCourses = sC.GetAll(x => x.ID).ToList();
            students = st.GetAll().ToList();
            studyPrograms = sp.GetAll(x => x?.StudyProgramsTitle ?? String.Empty)?.ToList() ?? new List<StudyProgram>();
            SelectFilters = new()
            {
                { nameof(CourseView.StudyProgramID), studyPrograms.Select(x => new SelectListItem(x.StudyProgramsTitle, x.ID)) },
                { nameof(CourseView.SemesterID), semestersRepo.GetAll(x => x.SeasonFull() ?? "Undefined").Select(x => new SelectListItem(x.SeasonFull(), x.ID)) },
            };
            CheckBoxFilters = new() { 
                nameof(CourseView.IsMandatory) 
            };
            
        }
        public override async Task<IActionResult> OnGetDetailsAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null) {

            var courseExists = coursesRepo?.GetAll(x => x.ID).FirstOrDefault(x => x.ID == id) != null;
            if(!courseExists) {
                var enrollment = enrollments.FirstOrDefault(x => x.ID == id);
                id = enrollment?.CourseID ?? id;
            }
                
            await base.OnGetDetailsAsync(id, pageIndex, itemsNr, currentFilter, sortOrder, StudyProgramID, SemesterID, Status, IsMandatory, nativePage);

            Item.SemesterID = semesterCourseR?.GetAll(x => x.ID)?.FirstOrDefault(x => x.CourseID == Item.ID)?.SemesterID ?? "Undefined";
            var studentInCourse = enrollmentsRepo?.GetAll(x => x.ID)?.FirstOrDefault(x => x.StudentID == SessionUserID && x.CourseID == Item.ID);

            StudentIsInCourse = studentInCourse != null;

            if (StudentIsInCourse) {
                var lecturer = studentInCourse?.Lecturer;
                if (lecturer?.FullName == "Undefined Undefined") Item.LecturerName = "Undefined";
                else Item.LecturerName = $"{lecturer?.FullName}";
                Item.EnrollmentID = studentInCourse?.ID;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDetailsAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null) {
            setAttributes(pageIndex, sortOrder, currentFilter, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage);
            var studentInCourse = enrollments?.FirstOrDefault(x => x.StudentID == SessionUserID && x.CourseID == id) ?? null;
            if (studentInCourse != null) {
                await enrollmentsRepo!.DeleteAsync(studentInCourse.ID);
            } else {
                var season = semesterCourses?.FirstOrDefault(x => x.CourseID == id) ?? null;
                var enr = new EnrollmentView() {
                    DegreeTakerID = Item.LecturerID,
                    CourseID = id,
                    Grade = 9,
                    ID = Guid.NewGuid().ToString(),
                    StudentID = SessionUserID,
                    SemesterID = season?.SemesterID ?? string.Empty
                };
                await enrollmentsRepo!.AddAsync(new EnrollmentViewFactory().Create(enr));
            }
            return redirectToIndex();
        }
        protected override CourseView toView(Course? entity, bool getNullVal = false) => (getNullVal) ? new CourseViewFactory().Create(entity) : new CourseViewFactory().CreateUndefined(entity);
        protected override Course toObject(CourseView? item) => new CourseViewFactory().Create(item);
    
    }
}
