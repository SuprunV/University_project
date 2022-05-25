using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.IsoEnums;

namespace Project_1.Tests.Data.IsoEnums {
    [TestClass] public class IsoSemesterTests : TypeTests {
        [TestMethod] public void AutumnTest() => doTest(IsoSemester.Autumn, 0, "Autumn");
        [TestMethod] public void SpringTest() => doTest(IsoSemester.Spring, 1, "Spring");
        [TestMethod] public void NotApplicableTest() => doTest(IsoSemester.NotApplicable, 9, "Not applicable");
        private static void doTest(IsoSemester isoSemester, int value, string description) {
            areEqual(value, (int)isoSemester);
            areEqual(description, isoSemester.Description());
        }
    }
}
