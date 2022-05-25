using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.Connection;

namespace Project_1.Tests.Data.Connection
{
    [TestClass]
    public class CourseLecturerDataTests : SealedClassTests<CourseLecturerData, UniqueData>
    {
        [TestMethod] public void CourseIDTest() => isProperty<string?>();
        [TestMethod] public void LecturerIDTest() => isProperty<string?>();
    }
}
