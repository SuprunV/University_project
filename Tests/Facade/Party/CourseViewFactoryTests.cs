using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party {
    [TestClass]
    public sealed class CourseViewFactoryTests : ViewFactoryTests
        <CourseViewFactory, CourseView, Course, CourseData>
    {
        protected override Course toObject(CourseData d) => new(d);

        [TestMethod] public override void CreateViewTest()
        {
            var d = GetRandom.Value<CourseData>();
            var e = new Course(d);
            var v = new CourseViewFactory().Create(e);
            isNotNull(v);
            areEqual(v.CourseTitle, e.CourseTitle);
            areEqual(v.Description, e.Description);
            areEqual(v.EAP, e.EAP);
            areEqual(v.FullString, e.CreateTotalString());
        }
        [TestMethod] public override void CreateTest() { base.CreateTest(); }
        [TestMethod] public override void CreateUndefinedTest() { base.CreateUndefinedTest(); }
        [TestMethod] public override void CreateObjectTest() { base.CreateObjectTest(); }
    }
}
