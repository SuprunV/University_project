using Project_1.Data.IsoEnums;
using Project_1.Data.Party;
using Project_1.Domain.Party;

namespace Project_1.Facade.Party
{
    public sealed class LecturerView:NamedView
    { }
    public sealed class LecturerViewFactory : BaseViewFactory<LecturerView, Lecturer, LecturerData>
    {
        protected override Lecturer toEntity(LecturerData d) => new(d);
        public override Lecturer Create(LecturerView? v)
        {
            v ??= new LecturerView();
            v.Sex ??= IsoGender.NotApplicable;
            return base.Create(v);
        }
        public override LecturerView Create(Lecturer? e)
        {
            var v = base.Create(e);
            v.FullName = e?.CreateTotalString();
            return v;
        }
        public override LecturerView CreateUndefined(Lecturer? e)
        {
            var v = base.CreateUndefined(e);
            v.FullName = e?.CreateTotalString();
            return v;
        }
    }
}

