using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.IsoEnums;

namespace Project_1.Tests.Data.IsoEnums {
    [TestClass] public class IsoCourseStatusTests : TypeTests {
        [TestMethod] public void PassedTest() => doTest(IsoCourseStatus.Passed, 5, "Passed");
        [TestMethod] public void FailedTest() => doTest(IsoCourseStatus.Failed, 0, "Failed");
        [TestMethod] public void UnknownTest() => doTest(IsoCourseStatus.Unknown, 11, "Unknown");
        [TestMethod] public void InProcessTest() => doTest(IsoCourseStatus.InProcess, 9, "In process");
        private static void doTest(IsoCourseStatus isoSemester, int value, string description) {
            areEqual(value, (int)isoSemester);
            areEqual(description, isoSemester.Description());
        }
    }
}
