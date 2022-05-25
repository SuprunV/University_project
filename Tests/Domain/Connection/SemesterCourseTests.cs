using System;
using System.Collections.Generic;
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
    public class SemesterCourseTests : SealedClassTests<SemesterCourse, UniqueEntity<SemesterCourseData>>
    {
        protected override SemesterCourse createObj() => new(GetRandom.Value<SemesterCourseData>());
        [TestMethod] public void CourseIDTest() => isReadOnly(obj.Data.CourseID);
        [TestMethod] public void SemesterIDTest() => isReadOnly(obj.Data.SemesterID);

        [TestMethod]
        public void CourseTest() => itemTest<ICoursesRepo, Course, CourseData>(
            obj.CourseID, d => new Course(d), () => obj.Course);
        [TestMethod]
        public void SemesterTest() => itemTest<ISemestersRepo, Semester, SemesterData>(
            obj.SemesterID, d => new Semester(d), () => obj.Semester);
    }
}
