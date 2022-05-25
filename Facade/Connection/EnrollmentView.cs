using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;

namespace Project_1.Facade.Connection
{
    public sealed class EnrollmentView:UniqueView
    {

        [Required][DisplayName("Student")] public string? StudentID { get; set; } = string.Empty;
        [DisplayName("Lecturer")] public string? DegreeTakerID { get; set; } = string.Empty;
        [DisplayName("Semester")] public string SemesterID { get; set; } = string.Empty;
        [DisplayName("Study program")] public string StudyProgramID { get; set; } = string.Empty;
        [DisplayName("Course")] public string CourseID { get; set; } = string.Empty;
        [Required][DisplayName("Grade"), Range(0, 5)] public int? Grade { get; set; } = 0;
        [DisplayName("Status")] public string? Status { get; set; } = string.Empty;
        [DisplayName("Info")] public string FullString { get; set; } = string.Empty;
        [DisplayName("Description")] public string Description { get; set; } = string.Empty;
    }
    public sealed class EnrollmentViewFactory : BaseViewFactory<EnrollmentView, Enrollment, EnrollmentData>
    {
        protected override Enrollment toEntity(EnrollmentData d) => new(d);
        public override EnrollmentView Create(Enrollment? e)
        {
            var v = base.Create(e);
            v.Status = e?.CreateStatus();
            v.SemesterID = e?.SemesterID ?? "Undefined";
            v.StudyProgramID = e?.StudyProgramID ?? "Undefined";
            if (v.Grade == 9) v.Grade = 0;
            return v;
        }
        public override EnrollmentView CreateUndefined(Enrollment? e)
        {
            var v = base.CreateUndefined(e);
            v.SemesterID = e?.SemesterID ?? "Undefined";
            v.StudyProgramID = e?.StudyProgramID ?? "Undefined";
            v.Status = e?.CreateStatus();
            if (v.Grade == 9)
            { v.Grade = 0; }
            return v;
        }
    }
}
