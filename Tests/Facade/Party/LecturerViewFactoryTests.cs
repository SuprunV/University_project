using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party {
    [TestClass]
    public class LecturerViewFactoryTests : ViewFactoryTests
        <LecturerViewFactory, LecturerView, Lecturer, LecturerData>
    {
        protected override Lecturer toObject(LecturerData d) => new(d);

        [TestMethod] public override void CreateViewTest()
        {
            var d = GetRandom.Value<LecturerData>();
            var e = new Lecturer(d);
            var v = new LecturerViewFactory().Create(e);
            isNotNull(v);
            areEqual(v.LastName, e.LastName);
            areEqual(v.ID, e.ID);
            areEqual(v.FirstName, e.FirstName);
            areEqual(v.Sex, e.Sex);
            areEqual(v.UniID, e.UniID);
            areEqual(v.FullName, e.CreateTotalString() );

        }
        [TestMethod] public override void CreateTest() { base.CreateTest(); }
        [TestMethod] public override void CreateUndefinedTest() { base.CreateUndefinedTest(); }
        [TestMethod] public override void CreateObjectTest() { base.CreateObjectTest(); }

    }
}
