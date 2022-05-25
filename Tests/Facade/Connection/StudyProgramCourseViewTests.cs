using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Facade;
using Project_1.Facade.Connection;

namespace Project_1.Tests.Facade.Connection
{
    [TestClass]
    public class StudyProgramCourseViewTests : SealedClassTests<StudyProgramCourseView, UniqueView>
    {
        [TestMethod] public void StudyProgramIDTest() => isRequired<string?>("Study program");
        [TestMethod] public void CourseIDTest() => isRequired<string?>("Course");
      
    }
    [TestClass]
    public class StudyProgramCourseViewFactoryTests : ViewFactoryTests
        <StudyProgramCourseViewFactory, StudyProgramCourseView, StudyProgramCourse, StudyProgramCourseData>
    {
        protected override StudyProgramCourse toObject(StudyProgramCourseData d) => new(d);
    }
}
