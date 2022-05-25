using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Infra;
using Project_1.Infra.Initializers;

namespace Project_1.Tests.Infra.Initializers
{
    [TestClass]
    public class BaseInitializerTests : AbstractClassTests<BaseInitializer<StudentData>, object>
    {
        private class testClass : BaseInitializer<StudentData>
        {
            public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            protected override IEnumerable<StudentData> getEntities => throw new System.NotImplementedException();
        }
        protected override BaseInitializer<StudentData> createObj()
        {
            var db = GetRepo.Instance<UniversityDb>();
            var set = db?.Students;
            return new testClass(db, set);
        }

        [TestMethod]
        public void InitTest()
        {
            var db = GetRepo.Instance<UniversityDb>();
            var a = new StudyProgramInitializer(db);
            a.Init();
            isNotNull(db?.Students);
        }
    }

    [TestClass]
    public class UniversityDbInitializerTests : TypeTests
    {
        [TestMethod]
        public void InitTest()
        {
            var db = GetRepo.Instance<UniversityDb>();
            var a = new StudyProgramInitializer(db);
            a.Init();
            isNotNull(db?.Students);
        }
    }
}
