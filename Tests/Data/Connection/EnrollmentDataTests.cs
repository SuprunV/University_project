using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.Connection;

namespace Project_1.Tests.Data.Connection
{
    [TestClass]
    public class EnrollmentDataTests : SealedClassTests<EnrollmentData, UniqueData>
    {
        [TestMethod] public void StudentIDTest() => isProperty<string?>();
        [TestMethod] public void DegreeTakerIDTest() => isProperty<string?>();
        [TestMethod] public void SemesterIDTest() => isProperty<string?>();
        [TestMethod] public void CourseIDTest() => isProperty<string?>();
        [TestMethod] public void GradeTest() => isProperty<int?>();
    }
}
