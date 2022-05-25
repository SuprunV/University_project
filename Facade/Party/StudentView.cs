using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Facade.Party
{
    public sealed class StudentView:NamedView
    {
        [DisplayName("Date of enrollment"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false), Required] public DateTime? EnrollmentDate { get; set; }
        [DisplayName("Nationality"),Required] public string? Nationality { get; set; }
        [DisplayName("Study program"), Required] public string? StudyProgramID { get; set; }
    }
    public sealed class StudentViewFactory : BaseViewFactory<StudentView, Student, StudentData>
    {
        protected override Student toEntity(StudentData d) => new(d);
        public override Student Create(StudentView? v)
        {
            v ??= new StudentView();
            v.Sex ??= IsoGender.NotApplicable;
            return base.Create(v);
        }
        public override StudentView Create(Student? e)
        {
            var v = base.Create(e);
            v.FullName = e?.CreateTotalString();
            return v;
        }
        public override StudentView CreateUndefined(Student? e)
        {
            var v = base.CreateUndefined(e);
            v.FullName = e?.CreateTotalString();
            return v;
        }
    }
}
