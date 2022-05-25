using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Connection
{
    [TestClass]
    public class CourseLecturerTests : SealedClassTests<CourseLecturer, UniqueEntity<CourseLecturerData>>
    {
        protected override CourseLecturer createObj() => new(GetRandom.Value<CourseLecturerData>());
        [TestMethod] public void CourseIDTest() => isReadOnly(obj.Data.CourseID);
        [TestMethod] public void LecturerIDTest() => isReadOnly(obj.Data.LecturerID);
        [TestMethod]
        public void CourseTest() => itemTest<ICoursesRepo, Course, CourseData>(
            obj.CourseID, d => new Course(d), () => obj.Course);
        [TestMethod]
        public void LecturerTest() => itemTest<ILecturersRepo, Lecturer, LecturerData>(
            obj.LecturerID, d => new Lecturer(d), () => obj.Lecturer);
    }
}
