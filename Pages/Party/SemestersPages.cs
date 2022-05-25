using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Aids;
using Project_1.Data.IsoEnums;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Pages.Party
{
    public class SemestersPages : PagedPage<SemesterView, Semester, ISemestersRepo>
    {
        public SemestersPages(ISemestersRepo r) : base(r) { }
        protected override Semester toObject(SemesterView? item) => new SemesterViewFactory().Create(item);
        protected override SemesterView toView(Semester? entity, bool getNullVal = false) => new SemesterViewFactory().Create(entity);
        public override string[] IndexColumns { get; } = new[] { 
            nameof(SemesterView.Season),
            nameof(SemesterView.DateStart),
            nameof(SemesterView.DateEnd),
        };
        public IEnumerable<SelectListItem> Seasons
         => Enum.GetValues<IsoSemester>()?
            .Select(x => new SelectListItem(x.Description(), x.ToString()))
            ?? new List<SelectListItem>();
        public string GenderDescription(IsoSemester? x)
            => (x ?? IsoSemester.NotApplicable).Description();
        public override object? GetValue(string name, SemesterView v) {
            var r = base.GetValue(name, v);
            return name == nameof(SemesterView.Season) ? GenderDescription((IsoSemester)(r ?? IsoSemester.NotApplicable)) : r;
        }
    }
}
