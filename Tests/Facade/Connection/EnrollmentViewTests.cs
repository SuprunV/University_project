using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Facade;
using Project_1.Facade.Connection;

namespace Project_1.Tests.Facade.Connection
{
    [TestClass]
    public class EnrollmentViewTests : SealedClassTests<EnrollmentView, UniqueView> {
        [TestMethod] public void StudentIDTest() => isRequired<string?>("Student");
        [TestMethod] public void DegreeTakerIDTest() => isDisplayNamed<string?>("Lecturer");
        [TestMethod] public void SemesterIDTest() => isDisplayNamed<string?>("Semester");
        [TestMethod] public void StudyProgramIDTest() => isDisplayNamed<string?>("Study program");
        [TestMethod] public void CourseIDTest() => isDisplayNamed<string?>("Course");
        [TestMethod] public void GradeTest() => isRequired<int?>("Grade");
        [TestMethod] public void StatusTest() => isDisplayNamed<string?>("Status");
        [TestMethod] public void FullStringTest() => isDisplayNamed<string?>("Info");
        [TestMethod] public void DescriptionTest() => isDisplayNamed<string?>("Description");
    }
    [TestClass]
    public class EnrollmentViewFactoryTests : ViewFactoryTests<EnrollmentViewFactory,EnrollmentView, Enrollment, EnrollmentData>
    {
      
        [TestMethod] public override void CreateViewTest()
        {
            var d = GetRandom.Value<EnrollmentData>();
            var e = new Enrollment(d);
            var v = new EnrollmentViewFactory().Create(e);
            isNotNull(v);
            areEqual(v.CourseID, e.CourseID);
            areEqual(v.DegreeTakerID, e.DegreeTakerID);
            areEqual(v.Grade, e.Grade);
            areEqual(v.Status, e.CreateStatus());
            areEqual(v.SemesterID, e.SemesterID);
            areEqual(v.StudentID, e.StudentID);
        }

        [TestMethod] public override void CreateTest() { base.CreateTest(); }
        [TestMethod] public override void CreateUndefinedTest() { base.CreateUndefinedTest(); }
        [TestMethod] public override void CreateObjectTest() { base.CreateObjectTest(); }
        protected override Enrollment toObject(EnrollmentData d) => new(d);
    }
}
