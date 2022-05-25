using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party {
    [TestClass]
    public class SemesterViewFactoryTests : ViewFactoryTests
        <SemesterViewFactory, SemesterView, Semester, SemesterData>
    {
        protected override Semester toObject(SemesterData d) => new(d);
        [TestMethod]
        public override void CreateViewTest()
        {
            var d = GetRandom.Value<SemesterData>();
            var e = new Semester(d);
            var v = new SemesterViewFactory().Create(e);
            isNotNull(v);
            areEqual(v.SeasonFull, e.SeasonFull());
            areEqual(v.Season, e.Season);
            areEqual(v.DateStart, e.DateStart);
            areEqual(v.DateEnd, e.DateEnd);
        }
        [TestMethod] public override void CreateTest() { base.CreateTest(); }
        [TestMethod] public override void CreateUndefinedTest() { base.CreateUndefinedTest(); }
        [TestMethod] public override void CreateObjectTest() { base.CreateObjectTest(); }
    }
}
