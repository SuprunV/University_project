using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Infra;
using Project_1.Infra.Connection;

namespace Project_1.Tests.Infra.Connection
{
    [TestClass] public class SemestersCourseRepoTests
        : SealedRepoTests<SemestersCoursesRepo, Repo<SemesterCourse, SemesterCourseData>, SemesterCourse, SemesterCourseData>
    {
        protected override SemestersCoursesRepo createObj() => new(GetRepo.Instance<UniversityDb>());
        protected override object? getSet(UniversityDb db) => db.SemestersCourse;
    }
}
