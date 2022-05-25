using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;

namespace Project_1.Facade.Connection
{
    public sealed class StudyProgramCourseView : UniqueView
    {
        [Required][DisplayName("Study program")] public string StudyProgramID { get; set; } = string.Empty;
        [Required][DisplayName("Course")] public string CourseID { get; set; } = string.Empty;

    }

    public sealed class StudyProgramCourseViewFactory : BaseViewFactory<StudyProgramCourseView, StudyProgramCourse, StudyProgramCourseData>
    {
        protected override StudyProgramCourse toEntity(StudyProgramCourseData d) => new(d);
    }
}
