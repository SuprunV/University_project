using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_1.Facade.Connection {
    public sealed class CourseLecturerView : UniqueView {
        [Required] [DisplayName("Course")] public string CourseID { get; set; } = string.Empty;
        [Required] [DisplayName("Lecturer")] public string LecturerID { get; set; } = string.Empty;
    }
    public sealed class CourseLecturerViewFactory : 
        BaseViewFactory<CourseLecturerView,CourseLecturer, CourseLecturerData> {
        protected override CourseLecturer toEntity(CourseLecturerData d) => new(d);
    }
}