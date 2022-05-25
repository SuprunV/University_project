using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Infra;
using Project_1.Infra.Initializers;

namespace Project_1.Tests.Infra.Initializers
{
    [TestClass]
    public class StudentInitializerTests
        : SealedBaseTests<StudentInitializer, BaseInitializer<StudentData>>
    {
        protected override StudentInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new StudentInitializer(db);
        }
    }
    [TestClass]
    public class CountryInitializerTests
        : SealedBaseTests<CountryInitializer, BaseInitializer<CountryData>>
    {
        protected override CountryInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new CountryInitializer(db);
        }
    }
    [TestClass]
    public class CourseInitializerTests
        : SealedBaseTests<CourseInitializer, BaseInitializer<CourseData>>
    {
        protected override CourseInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new CourseInitializer(db);
        }
    }
    [TestClass]
    public class CourseLecturerInitializerTests
        : SealedBaseTests<CourseLecturerInitializer, BaseInitializer<CourseLecturerData>>
    {
        protected override CourseLecturerInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new CourseLecturerInitializer(db);
        }
    }
    [TestClass]
    public class LecturerInitializerTests
        : SealedBaseTests<LecturerInitializer, BaseInitializer<LecturerData>>
    {
        protected override LecturerInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new LecturerInitializer(db);
        }
    }
    [TestClass]
    public class SemesterInitializerTests
        : SealedBaseTests<SemesterInitializer, BaseInitializer<SemesterData>>
    {
        protected override SemesterInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new SemesterInitializer(db);
        }
    }
    [TestClass]
    public class StudyProgramInitializerTests
        : SealedBaseTests<StudyProgramInitializer, BaseInitializer<StudyProgramData>>
    {
        protected override StudyProgramInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new StudyProgramInitializer(db);
        }
    }
    [TestClass]
    public class StudyProgramCoursesInitializerTests
        : SealedBaseTests<StudyProgramCoursesInitializer, BaseInitializer<StudyProgramCourseData>>
    {
        protected override StudyProgramCoursesInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new StudyProgramCoursesInitializer(db);
        }
    }
    [TestClass]
    public class SemesterCoursesInitializerTests
        : SealedBaseTests<SemesterCoursesInitializer, BaseInitializer<SemesterCourseData>>
    {
        protected override SemesterCoursesInitializer createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            return new SemesterCoursesInitializer(db);
        }
    }

}
