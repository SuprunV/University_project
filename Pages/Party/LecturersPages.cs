using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Data.Party;
using Project_1.Aids;
using Project_1.Data.IsoEnums;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Pages.Party
{
    public class LecturersPages : PagedPage<LecturerView, Lecturer, ILecturersRepo>
    {
        public LecturersPages(ILecturersRepo r) : base(r) { }
        protected override Lecturer toObject(LecturerView? item) => new LecturerViewFactory().Create(item);
        protected override LecturerView toView(Lecturer? entity, bool getNullVal = false) => (getNullVal) ? new LecturerViewFactory().Create(entity) : new LecturerViewFactory().CreateUndefined(entity);
        public override string[] IndexColumns { get; } = new[] {
            nameof(LecturerView.UniID),
            nameof(LecturerView.FirstName),
            nameof(LecturerView.LastName),
            nameof(LecturerView.Sex)
        };
        public IEnumerable<SelectListItem> Genders
            => Enum.GetValues<IsoGender>()?
                   .Select(x => new SelectListItem(x.Description(), x.ToString()))
               ?? new List<SelectListItem>();
        public string GenderDescription(IsoGender? x)
            => (x ?? IsoGender.NotApplicable).Description();
        public override object? GetValue(string name, LecturerView v)
        {
            var r = base.GetValue(name, v);
            return name == nameof(LecturerView.Sex) ? GenderDescription((IsoGender)(r ?? IsoGender.NotKnown)) : r;
        }
    }
}
