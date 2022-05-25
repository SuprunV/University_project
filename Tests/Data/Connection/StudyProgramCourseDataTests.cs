using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.Connection;

namespace Project_1.Tests.Data.Connection
{
    [TestClass]
    public class StudyProgramCourseDataTests : SealedClassTests<StudyProgramCourseData, UniqueData>
    {
        [TestMethod] public void StudyProgramIDTest() => isProperty<string?>();
        [TestMethod] public void CourseIDTest() => isProperty<string?>();
    }
}
