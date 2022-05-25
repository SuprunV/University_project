using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party {
    [TestClass]
    public class StudentViewFactoryTests : ViewFactoryTests
        <StudentViewFactory, StudentView, Student, StudentData>
    {
        protected override Student toObject(StudentData d) => new(d);
        [TestMethod] public override void CreateViewTest()
        {
            var d = GetRandom.Value<StudentData>();
            var e = new Student(d);
            var v = new StudentViewFactory().Create(e);
            isNotNull(v);
            areEqual(v.Nationality, e.Nationality);
            areEqual(v.ID, e.ID);
            areEqual(v.Sex, e.Sex);
            areEqual(v.UniID, e.UniID);
            areEqual(v.FirstName, e.FirstName);
            areEqual(v.LastName, e.LastName); 
            areEqual(v.EnrollmentDate, e.EnrollmentDate);
            areEqual(v.FullName, e.CreateTotalString());
        }
        [TestMethod] public override void CreateTest() { base.CreateTest(); }
        [TestMethod] public override void CreateUndefinedTest() { base.CreateUndefinedTest(); }
        [TestMethod] public override void CreateObjectTest() { base.CreateObjectTest(); }
    }
}
