using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Facade;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party
{
    [TestClass]
    public class CourseViewTests : SealedClassTests<CourseView, UniqueView> {
        [TestMethod] public void CourseTitleTest() => isRequired<string?>("Course title");
        [TestMethod] public void EAPTest() => isRequired<int?>("EAP");
        [TestMethod] public void IsMandatoryTest() => isDisplayNamed<string?>("Is mandatory");
        [TestMethod] public void DescriptionTest() => isDisplayNamed<string?>("Description");
        [TestMethod] public void FullStringTest() => isDisplayNamed<string?>("Info");
        [TestMethod] public void OwnerIDTest() => isDisplayNamed<string?>("Owner");
        [TestMethod] public void LecturerNameTest() => isDisplayNamed<string?>("Your lecturer");
        [TestMethod] public void StudyProgramIDTest() => isDisplayNamed<string?>("Study program");
        [TestMethod] public void SemesterIDTest() => isDisplayNamed<string?>("Semester");
        [TestMethod] public void LecturerIDTest() => isDisplayNamed<string?>("Allowed degree takers");
        [TestMethod] public void EnrollmentIDTest() => isDisplayNamed<string?>("Enrollment");
        

    }
}
