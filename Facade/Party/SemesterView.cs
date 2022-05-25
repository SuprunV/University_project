using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Facade.Party
{
    public sealed class SemesterView:UniqueView
    {
        [DisplayName("Season"), Required] public IsoSemester? Season { get; set; }
        [DisplayName("Info")] public string? SeasonFull { get; set; }
        [DisplayName("Start"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false),Required] public DateTime? DateStart { get; set; }
        [DisplayName("End"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = false),Required] public DateTime? DateEnd { get; set; }
    }
    public sealed class SemesterViewFactory : BaseViewFactory<SemesterView, Semester, SemesterData>
    {
        protected override Semester toEntity(SemesterData d) => new(d);
        public override Semester Create(SemesterView? v)
        {
            v ??= new SemesterView();
            v.Season ??= IsoSemester.NotApplicable;
            return base.Create(v);
        }
        public override SemesterView Create(Semester? e)
        {
            var v = base.Create(e);
            v.SeasonFull = e?.SeasonFull();
            return v;
        }
        public override SemesterView CreateUndefined(Semester? e)
        {
            var v = base.CreateUndefined(e);
            v.SeasonFull = e?.SeasonFull();
            return v;
        }
    }
}
