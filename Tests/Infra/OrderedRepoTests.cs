using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;

namespace Project_1.Tests.Infra {
    [TestClass] public class OrderedRepoTests
        : AbstractClassTests<OrderedRepo<Student, StudentData>, FilteredRepo<Student, StudentData>> {
        private class testClass : OrderedRepo<Student, StudentData> {
            public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            protected internal override Student toDomain(StudentData d) => new(d);
        }
        protected override OrderedRepo<Student, StudentData> createObj() {
            var db = GetRepo.Instance<UniversityDb>();
            var set = db?.Students;
            return new testClass(db, set);
        }
        [TestMethod] public void CurrentOrderTest() => isProperty<string?>();
        [TestMethod] public void DescendingStringTest() => areEqual("_desc", testClass.DescendingString);
        [DataRow(null, true)]
        [DataRow(null, false)]
        [DataRow(nameof(Student.UniID), true)]
        [DataRow(nameof(Student.UniID), false)]
        [DataRow(nameof(Student.Nationality), true)]
        [DataRow(nameof(Student.Nationality), false)]
        [DataRow(nameof(Student.Sex), true)]
        [DataRow(nameof(Student.Sex), false)]
        [DataRow(nameof(Student.FirstName), true)]
        [DataRow(nameof(Student.FirstName), false)]
        [DataRow(nameof(Student.LastName), true)]
        [DataRow(nameof(Student.LastName), false)]
        [DataRow(nameof(Student.EnrollmentDate), true)]
        [DataRow(nameof(Student.EnrollmentDate), false)]
        [TestMethod] public void CreateSqlTest(string s, bool isDescending) { 
            obj.CurrentOrder = (s is null)? s: isDescending? s + testClass.DescendingString: s;
            var q = obj.createSql();
            var actual = q.Expression.ToString();
            if (s is null) isTrue(actual.EndsWith(".Select(s => s)"));
            else if (isDescending) isTrue(actual.EndsWith(
                $".Select(s => s).OrderByDescending(x => Convert(x.{s}, Object))"));
            else isTrue(actual.EndsWith(
                $".Select(s => s).OrderBy(x => Convert(x.{s}, Object))"));
        }

        [DataRow(true, true)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(false, false)]
        [TestMethod] public void SortOrderTest(bool isDescending, bool isSame) {
            var s = GetRandom.String();
            var c = isSame ? s : GetRandom.String();
            obj.CurrentOrder = isDescending ? c + testClass.DescendingString : c;
            var actual = obj.SortOrder(s);
            var sDes = s+testClass.DescendingString;
            var expected = isSame ? (isDescending? s: sDes) : sDes;
            areEqual(expected, actual);
        }
    }
}

