using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Facade;
using Project_1.Facade.Connection;

namespace Project_1.Tests.Facade.Connection
{
    [TestClass]
    public class CourseLecturerViewTests : SealedClassTests<CourseLecturerView, UniqueView> {
        [TestMethod] public void CourseIDTest() => isRequired<string?>("Course");
        [TestMethod] public void LecturerIDTest() => isRequired<string?>("Lecturer");
    }
    [TestClass]
    public class CourseLecturerViewFactoryTests : ViewFactoryTests
        <CourseLecturerViewFactory, CourseLecturerView, CourseLecturer, CourseLecturerData>
    {
        protected override CourseLecturer toObject(CourseLecturerData d) => new(d);
    }
}
