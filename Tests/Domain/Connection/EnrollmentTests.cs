using System;
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
    public class EnrollmentTests : SealedClassTests<Enrollment, UniqueEntity<EnrollmentData>>
    {
        protected override Enrollment createObj() => new(GetRandom.Value<EnrollmentData>());
        [TestMethod] public void CourseIDTest() => isReadOnly(obj.Data.CourseID);
        [TestMethod] public void SemesterIDTest() => isReadOnly(obj.Data.SemesterID);
        [TestMethod] public void StudentIDTest() => isReadOnly(obj.Data.StudentID);
        [TestMethod] public void GradeTest() => isReadOnly(obj.Data.Grade);
        [TestMethod] public void DegreeTakerIDTest() => isReadOnly(obj.Data.DegreeTakerID);
        [TestMethod] public void StudyProgramIDTest() => isReadOnly(obj.StudyProgramID);
        [TestMethod] public void CreateStatusTest()
        {
            obj.Data.Grade = 1;
            var expected = $"Passed";
            areEqual(expected, obj.CreateStatus());
        }
        [TestMethod]
        public void CreateStatus1Test()
        {
            obj.Data.Grade = 0;
            var expected = $"Not passed";
            areEqual(expected, obj.CreateStatus());
        }
        [TestMethod]
        public void CourseTest() => itemTest<ICoursesRepo, Course, CourseData>(
            obj?.CourseID ?? String.Empty, d => new Course(d), () => obj?.Course);
        [TestMethod]
        public void LecturerTest() => itemTest<ILecturersRepo, Lecturer, LecturerData>(
            obj?.DegreeTakerID ?? String.Empty, d => new Lecturer(d), () => obj?.Lecturer);
        [TestMethod]
        public void StudentTest() => itemTest<IStudentsRepo, Student, StudentData>(
            obj.StudentID ?? String.Empty, d => new Student(d), () => obj?.Student);
        [TestMethod]
        public void SemesterTest() => itemTest<ISemestersRepo, Semester, SemesterData>(
            obj.SemesterID , d => new Semester(d), () => obj?.Semester);

    }
}
