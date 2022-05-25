using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Facade;
using Project_1.Facade.Connection;

namespace Project_1.Tests.Facade.Connection
{
    [TestClass]
    public class SemesterCourseViewTests : SealedClassTests<SemesterCourseView, UniqueView>
    {
        [TestMethod] public void SemesterIDTest() => isRequired<string?>("Semester");
        [TestMethod] public void CourseIDTest() => isRequired<string?>("Course");
    }
    [TestClass]
    public class SemesterCourseViewFactoryTests : ViewFactoryTests
        <SemesterCourseViewFactory, SemesterCourseView, SemesterCourse, SemesterCourseData>
    {
        protected override SemesterCourse toObject(SemesterCourseData d) => new(d);
    }
}