using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;

namespace Project_1.Tests.Infra {
    [TestClass] public class FilteredRepoTests 
        : AbstractClassTests<FilteredRepo<Student, StudentData>, CrudRepo<Student, StudentData>> {
        private class testClass : FilteredRepo<Student, StudentData> {
            public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            protected internal override Student toDomain(StudentData d) => new(d);
        }
        protected override FilteredRepo<Student, StudentData> createObj() {
            var db = GetRepo.Instance<UniversityDb>();
            var set = db?.Students;
            return new testClass(db, set);
        }
        [TestMethod] public void CurrentFilterTest() => isProperty<string>();
        [TestMethod] public async Task getItemsCountTest()
        { 
            var list = await obj.runSql(obj.addFilter(obj.addAdvancedFilter(from s in obj.set select s)));
           areEqual(0, list.Count);
        }
        [DataRow(true)]
        [DataRow(false)]
        [TestMethod] public void CreateSqlTest(bool hasCurrentFilter) {
            obj.CurrentFilter = hasCurrentFilter ? GetRandom.String(): null;
            var q1 = obj.createSql();
            var q2 = obj.addFilter(q1);
            areEqual(q1, q2);
            var s = q1.Expression.ToString();
            isTrue(s.EndsWith(".Select(s => s)"));
        }
    }
}

