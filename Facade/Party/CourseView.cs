using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Facade.Party
{
    public sealed class CourseView : UniqueView
    {
        [DisplayName("Course title"), Required, RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Word starts with a capital letter and use only english keyboard")] public string? CourseTitle { get; set; }
        [DisplayName("EAP"), Required, Range(1, 12)] public int? EAP { get; set; }
        [DisplayName("Owner")] public string? OwnerID { get; set; }
        [DisplayName("Description"), RegularExpression(@"^[a-zA-Z0-9 , . : ; - ""'\s-]*$", ErrorMessage = "Use only english keyboard")] public string? Description { get; set; }
        [DisplayName("Info")] public string? FullString { get; set; }
        [DisplayName("Is mandatory")] public string? IsMandatory { get; set; }
        [DisplayName("Your lecturer")] public string? LecturerName { get; set; }
        [DisplayName("Allowed degree takers")] public string? LecturerID { get; set; }
        [DisplayName("Study program")] public string? StudyProgramID { get; set; }
        [DisplayName("Semester")] public string? SemesterID { get; set; }
        [DisplayName("Enrollment")] public string? EnrollmentID { get; set; } 
    }
    public sealed class CourseViewFactory : BaseViewFactory<CourseView, Course, CourseData>
    {
        protected override Course toEntity(CourseData d) => new(d);
        public override CourseView Create(Course? e)
        {
            var v = base.Create(e);
            v.FullString = e?.CreateTotalString();
            v.StudyProgramID = e?.studyProgramID;
            v.SemesterID = e?.semesterID;
            v.LecturerID = e?.lecturer?.ID ;
            return v;
        }
        public override CourseView CreateUndefined(Course? e)
        {
            var v = base.CreateUndefined(e);
            v.FullString = e?.CreateTotalString();
            v.StudyProgramID = e?.studyProgramID;
            v.SemesterID = e?.semesterID;
            v.LecturerID = e?.lecturer?.ID;
            return v;
        }
    }
}
