using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;

namespace Project_1.Facade.Connection
{
    public sealed class SemesterCourseView:UniqueView
    {
        [Required][DisplayName("Semester")] public string SemesterID { get; set; } = string.Empty;
        [Required][DisplayName("Course")] public string CourseID { get; set; } = string.Empty;

    }

    public sealed class SemesterCourseViewFactory : BaseViewFactory<SemesterCourseView, SemesterCourse, SemesterCourseData>
    {
        protected override SemesterCourse toEntity(SemesterCourseData d) => new(d);
    }
}
