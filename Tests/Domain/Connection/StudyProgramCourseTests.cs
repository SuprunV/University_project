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
    public class StudyProgramCourseTests : SealedClassTests<StudyProgramCourse, UniqueEntity<StudyProgramCourseData>>
    {
        protected override StudyProgramCourse createObj() => new(GetRandom.Value<StudyProgramCourseData>());
        [TestMethod] public void CourseIDTest() => isReadOnly(obj.Data.CourseID);
        [TestMethod] public void StudyProgramIDTest() => isReadOnly(obj.Data.StudyProgramID);
        [TestMethod] public void CourseTest() => itemTest<ICoursesRepo, Course, CourseData>(
            obj?.CourseID ?? String.Empty, d => new Course(d), () => obj?.Course);
        [TestMethod]
        public void StudyProgramTest() => itemTest<IStudyProgramsRepo, StudyProgram, StudyProgramData>(
            obj?.StudyProgramID ?? String.Empty, d => new StudyProgram(d), () => obj?.StudyProgram);

    }
}
