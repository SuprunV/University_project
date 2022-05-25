using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.Party;

namespace Project_1.Tests.Data.Party
{
    [TestClass] public class CourseDataTests: SealedClassTests<CourseData,UniqueData> {
        [TestMethod] public void CourseTitleTest() => isProperty<string?>();
        [TestMethod] public void EAPTest() => isProperty<int?>();
        [TestMethod] public void DescriptionTest() => isProperty<string?>();
        [TestMethod] public void OwnerIDTest() => isProperty<string?>();
    }
}
