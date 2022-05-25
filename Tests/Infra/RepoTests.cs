using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;
using Project_1.Infra.Initializers;
using Project_1.Tests.Data.Party;

namespace Project_1.Tests.Infra {
    [TestClass]
    public class RepoTests : AbstractClassTests<Repo<Student, StudentData>, PagedRepo<Student, StudentData>>
    {
        private class TestClass : Repo<Student, StudentData>
        {
            public TestClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            protected internal override Student toDomain(StudentData d) => new(d);
        }
        [TestClass]
        public class PagedRepoTests
            : AbstractClassTests<PagedRepo<Student, StudentData>, OrderedRepo<Student, StudentData>>
        {
            private class testClass : PagedRepo<Student, StudentData>
            {
                public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
                protected internal override Student toDomain(StudentData d) => new(d);
            }
            protected override PagedRepo<Student, StudentData> createObj()
            {
                var db = GetRepo.Instance<UniversityDb>();
                var set = db?.Students;
                return new testClass(db, set);
            }
            [TestMethod] public void PageIndexTest() => isProperty<int?>();
            [TestMethod] public void TotalPagesTest() => isReadOnly<int?>();
            [TestMethod] public void HasNextPageTest() => isReadOnly<bool?>();
            [TestMethod] public void HasPreviousPageTest() => isReadOnly<bool>();
            [TestMethod] public void PageSizeTest() => isProperty<int?>();

        }

        protected override Repo<Student, StudentData> createObj() => new TestClass(null, null);
        
        [TestClass]
        public class UniversityDbTests : SealedBaseTests<UniversityDb, DbContext>
        {
            protected override UniversityDb createObj() => new(new DbContextOptions<UniversityDb>());
            protected override void isSealedTest() => isTrue(obj?.GetType()?.IsSealed ?? false);
            [TestMethod] public void StudentsTest() => isProperty<DbSet<StudentData>>();
            [TestMethod] public void CoursesTest() => isProperty<DbSet<CourseData>>();
            [TestMethod] public void LecturersTest() => isProperty<DbSet<LecturerData>>();
            [TestMethod] public void StudyProgramsTest() => isProperty<DbSet<StudyProgramData>>();
            [TestMethod] public void SemestersTest() => isProperty<DbSet<SemesterData>>();
            [TestMethod] public void CountriesTest() => isProperty<DbSet<CountryData>>();
            [TestMethod] public void CourseLecturersTest() => isProperty<DbSet<CourseLecturerData>>();
            [TestMethod] public void StudyProgramsCoursesTest() => isProperty<DbSet<StudyProgramCourseData>>();
            [TestMethod] public void EnrollmentsTest() => isProperty<DbSet<EnrollmentData>>();
            [TestMethod] public void SemestersCourseTest() => isProperty<DbSet<SemesterCourseData>>();
            [TestMethod]
            public void InitializeTablesTest()
            {
                var b = new ModelBuilder();
                UniversityDb.InitializeTables(b);
                isNotNull(b.Entity<StudentData>());
            }
        }
    }

}