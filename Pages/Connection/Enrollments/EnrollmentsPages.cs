using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Aids;
using Project_1.Data.IsoEnums;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;

namespace Project_1.Pages.Connection.Enrollments
{
    public class EnrollmentsPages : PagedPage<EnrollmentView, Enrollment, IEnrollmentsRepo>
    {
        public List<Lecturer>? alLecturers;
        public List<Course>? allCourses;
        public List<Semester>? allSemesters;
        public List<Student>? allStudents;
        public List<SemesterCourse>? allSemesterCourses;
        public List<CourseLecturer>? allCourseLecturers; 
        public List<StudyProgram>? allStudyPrograms;
        public override Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }
        public EnrollmentsPages(IEnrollmentsRepo r, IStudentsRepo students, 
            ICourseLecturerRepo courseLecturer, 
            ISemesterCourseRepo semesterCourse,
            ISemestersRepo semesters, ICoursesRepo courses, ILecturersRepo lecturers, IStudyProgramsRepo sp) : base(r)
        {
            alLecturers = lecturers?.GetAll(l => l.LastName ?? String.Empty);
            allCourses = courses?.GetAll(c => c.CourseTitle ?? String.Empty);
            allSemesters = semesters?.GetAll(s => s.SeasonFull() ?? String.Empty);
            allStudents = students?.GetAll(s => s.LastName ?? String.Empty);
            allSemesterCourses = semesterCourse?.GetAll(c => c.ID);
            allCourseLecturers = courseLecturer?.GetAll(c => c.ID);
            allStudyPrograms = sp?.GetAll(s => s.ID);
            SelectFilters = new()
            {
                { nameof(EnrollmentView.Status), Enum.GetValues<IsoCourseStatus>().Select(x => new SelectListItem(x.Description(), x.ToString())) },
                { nameof(CourseView.SemesterID), allSemesters!.Select(x => new SelectListItem(x.SeasonFull(), x.ID)) },
            };
        }
        protected override EnrollmentView toView(Enrollment? entity, bool getNullVal = false) => (getNullVal) ? new EnrollmentViewFactory().Create(entity): new EnrollmentViewFactory().CreateUndefined(entity);

        protected override Enrollment toObject(EnrollmentView? item) => new EnrollmentViewFactory().Create(item);
        public override string[] IndexColumns { get; } = new[] {
            nameof(EnrollmentView.StudentID),
            nameof(EnrollmentView.SemesterID),
            nameof(EnrollmentView.StudyProgramID),
            nameof(EnrollmentView.CourseID),
            nameof(EnrollmentView.Grade),
            nameof(EnrollmentView.Status),
            nameof(EnrollmentView.DegreeTakerID),

        };
        public IEnumerable<SelectListItem> Semesters
            => allSemesters?
                   .Select(x => new SelectListItem(x.SeasonFull(), x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Courses
            => allCourses?
                   .Select(x => new SelectListItem(x.CourseTitle, x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Lecturers
            => alLecturers?
                   .Select(x => new SelectListItem(x.FullName, x.ID))
               ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Students
            => allStudents?
                   .Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.ID))
               ?? new List<SelectListItem>();

        public string StudyProgramName(string? semID = null)
            => allStudyPrograms?.FirstOrDefault(x => x.ID == (semID ?? string.Empty))?.StudyProgramsTitle ?? "Unspecified";
        public string SemesterName(string? semID = null)
            => Semesters?.FirstOrDefault(x => x.Value == (semID ?? string.Empty))?.Text ?? "Unspecified";
        public string CourseName(string? courID = null)
            => Courses?.FirstOrDefault(x => x.Value == (courID ?? string.Empty))?.Text ?? "Unspecified";
        public string LecturerName(string? lecID = null)
            => 
                Lecturers?.FirstOrDefault(x => x.Value == (lecID ?? string.Empty))?.Text ?? "Unspecified";
        public string StudentName(string? studID = null)
            => Students?.FirstOrDefault(x => x.Value == (studID ?? string.Empty))?.Text ?? "Unspecified";

        public override object? GetValue(string name, EnrollmentView v)
        {
            var r = base.GetValue(name, v);
            return name == nameof(EnrollmentView.SemesterID) ? SemesterName(r as string)
                : name == nameof(EnrollmentView.CourseID) ? CourseName(r as string)
                : name == nameof(EnrollmentView.DegreeTakerID) ? LecturerName(r as string)
                : name == nameof(EnrollmentView.StudentID) ? StudentName(r as string)
                : name == nameof(EnrollmentView.StudyProgramID) ? StudyProgramName(r as string)
                : r;
        }
    }
}
