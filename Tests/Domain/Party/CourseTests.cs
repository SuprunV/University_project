using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass] public class CourseTests : SealedClassTests<Course, UniqueEntity<CourseData>>
    {
        protected override Course createObj() => new(GetRandom.Value<CourseData>());
        [TestMethod] public void CourseTitleTest() => isReadOnly(obj.Data.CourseTitle);
        [TestMethod] public void EAPTest() => isReadOnly(obj.Data.EAP);
        [TestMethod] public void DescriptionTest() => isReadOnly(obj.Data.Description);
        [TestMethod] public void OwnerIDTest() => isReadOnly(obj.Data.OwnerID);
        [TestMethod] public void studyProgramIDTest() => isReadOnly(obj.studyProgramID);
        [TestMethod] public void semesterIDTest() => isReadOnly(obj.semesterID);
        [TestMethod] public void lecturerIDTest() => isReadOnly(obj.lecturerID);
        [TestMethod] public void CreateTotalStringTest()
        {
            var expected = $"{obj.CourseTitle} ({obj.EAP} EAP, Owner of course: Undefined)";
            areEqual(expected, obj.CreateTotalString());
        }
        [TestMethod]
        public void lecturerTest() => itemTest<ILecturersRepo, Lecturer, LecturerData>(
            obj?.OwnerID ?? String.Empty, d => new Lecturer(d), () => obj?.lecturer);
    }

}
