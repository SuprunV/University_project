using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Facade.Connection;

namespace Project_1.Pages.Connection {
    public class CourseLecturersPages : PagedPage<CourseLecturerView, CourseLecturer, ICourseLecturerRepo> {

        //fileds, that contains values from DB
        private readonly ILecturersRepo lecturers;
        private readonly ICoursesRepo courses;
        public CourseLecturersPages(ICourseLecturerRepo r, ICoursesRepo c, ILecturersRepo l) : base(r) {
            lecturers = l;
            courses = c;
        }
        
        protected override CourseLecturer toObject(CourseLecturerView? item) => new CourseLecturerViewFactory().Create(item);
        protected override CourseLecturerView toView(CourseLecturer? entity, bool getNullVal = false) => (getNullVal) ? new CourseLecturerViewFactory().Create(entity) : new CourseLecturerViewFactory().CreateUndefined(entity);

        public override string[] IndexColumns { get; } = new[] {
            nameof(CourseLecturer.CourseID),
            nameof(CourseLecturer.LecturerID)
        };

        public IEnumerable<SelectListItem> Courses
            => courses?.GetAll(x => x.CourseTitle ?? String.Empty)
            .Select(x => new SelectListItem(x.CourseTitle, x.ID))
            ?? new List<SelectListItem>();
        public IEnumerable<SelectListItem> Lecturers
            => lecturers?.GetAll(x => x.LastName ?? String.Empty)
            .Select(x => new SelectListItem($"{x.LastName} {x.FirstName}", x.ID))
            ?? new List<SelectListItem>();

        public string getName(IEnumerable<SelectListItem> list, string? id = null) =>
            list?.FirstOrDefault(x => x.Value == (id ?? string.Empty))?.Text ?? "Unspecified";

        public override object? GetValue(string name, CourseLecturerView v) {
            var r = base.GetValue(name, v);
            return name == nameof(CourseLecturerView.CourseID) ? getName(Courses, (r as string)) :
                name == nameof(CourseLecturerView.LecturerID) ? getName(Lecturers, (r as string)) :
                r;
            }   

    }
}