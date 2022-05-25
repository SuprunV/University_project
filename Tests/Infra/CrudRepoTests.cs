using System;
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
    [TestClass] public class CrudRepoTests
        : AbstractClassTests<CrudRepo<Student, StudentData>, BaseRepo<Student, StudentData>> {
        private UniversityDb? db;
        private DbSet<StudentData>? set;
        private StudentData? d;
        private Student? a;
        private int cnt;

        private class testClass : CrudRepo<Student, StudentData> {
            public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            protected internal override Student toDomain(StudentData d) => new(d);
        }
        protected override CrudRepo<Student, StudentData> createObj() {
            db = GetRepo.Instance<UniversityDb>();
            set = db?.Students;
            isNotNull(set);
            return new testClass(db, set);
        }
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            initSet();
            initAdr();
        }
        private void initSet() {
            cnt = GetRandom.Int32(5, 15);
            for (var i = 0; i < cnt; i++) set?.Add(GetRandom.Value<StudentData>());
            _ = db?.SaveChanges();
        }
        private void initAdr() {
            d = GetRandom.Value<StudentData>();
            isNotNull(d);
            a = new Student(d);
            var x = obj.Get(d.ID);
            isNotNull(x);
            areNotEqual(d.ID, x.ID);
        }
        [TestMethod] public async Task AddTest() {
            isNotNull(a);
            isNotNull(set);
            _ = obj?.Add(a);
            areEqual(cnt + 1, await set.CountAsync());
        }
        [TestMethod] public async Task AddAsyncTest() {
            isNotNull(a);
            isNotNull(set);
            _ = await obj.AddAsync(a);
            areEqual(cnt + 1, await set.CountAsync());
        }
        [TestMethod] public async Task DeleteTest() {
            isNotNull(d);
            await GetTest();
            _ = obj.Delete(d.ID);
            var x = obj.Get(d.ID);
            isNotNull(x);
            areNotEqual(d.ID, x.ID);
        }
        [TestMethod] public async Task DeleteAsyncTest() {
            isNotNull(d);
            await GetTest();
            _ = await obj.DeleteAsync(d.ID);
            var x = obj.Get(d.ID);
            isNotNull(x);
            areNotEqual(d.ID, x.ID);
        }
        [TestMethod] public async Task GetTest() {
            isNotNull(d);
            await AddTest();
            var x = obj.Get(d.ID);
            arePropertiesEqual(d, x.Data);
        }
        [TestMethod]
        public async Task GetByTest()
        {
            isNotNull(d);
            await AddTest();
            var x = obj.Get(d.ID);
            arePropertiesEqual(d, x.Data);
        }

        [DataRow(nameof(Student.ID))]
        [DataRow(nameof(Student.UniID))]
        [DataRow(nameof(Student.FirstName))]
        [DataRow(nameof(Student.LastName))]    
        [DataRow(nameof(Student.Sex))]
        [DataRow(nameof(Student.Nationality))]
        [DataRow(nameof(Student.EnrollmentDate))]
        [DataRow(nameof(Student.CreateTotalString))]
        [DataRow(null)]
        [TestMethod] public void GetAllTest(string s) {
            Func<Student, dynamic>? orderBy = null;
            if (s is nameof(Student.ID)) orderBy = x => x.ID;
            else if (s is nameof(Student.UniID)) orderBy = x => x?.UniID ?? String.Empty;
            else if (s is nameof(Student.FirstName)) orderBy = x => x?.FirstName ?? String.Empty;
            else if (s is nameof(Student.LastName)) orderBy = x => x?.LastName ?? String.Empty;
            else if (s is nameof(Student.FullName)) orderBy = x => x?.FullName ?? String.Empty;
            else if (s is nameof(Student.Sex)) orderBy = x => x.Sex;
            else if (s is nameof(Student.EnrollmentDate)) orderBy = x => x?.EnrollmentDate ?? DateTime.MinValue;
            else if (s is nameof(Student.CreateTotalString)) orderBy = x => x.CreateTotalString();
            var l = obj.GetAll(orderBy);
            areEqual(cnt, l.Count);
            if (orderBy is null) return;
            for(var i = 0; i < l.Count-1; i++) {
                var a = l[i];
                var b = l[i+1];
                var aX = orderBy(a) as IComparable;
                var bX = orderBy(b) as IComparable;
                isNotNull(aX);
                isNotNull(bX);
                var r = aX.CompareTo(bX);
                isTrue(r <= 0);
            }
        }

        [TestMethod] public async Task getItemsCountTest()
        {
            var l = await obj.runSql(from s in obj.set select s);
            areEqual(cnt,l.Count);
        }

        [TestMethod] public void GetListTest() {
            var l = obj.Get();
            areEqual(cnt, l.Count);
        }
        [TestMethod] public async Task GetAsyncTest() {
            isNotNull(d);
            await AddTest();
            var x = await obj.GetAsync(d.ID);
            arePropertiesEqual(d, x.Data);
        }
        [TestMethod] public async Task GetListAsyncTest() {
            var l = await obj.GetAsync();
            areEqual(cnt, l.Count);
        }
        [TestMethod] public async Task UpdateTest() {
            await GetTest();
            var dX = GetRandom.Value<StudentData>() as StudentData;
            isNotNull(d);
            isNotNull(dX);
            dX.ID = d.ID;
            var aX = new Student(dX);
            _ = obj.Update(aX);
            var x = obj.Get(d.ID);
            areEqualProperties(dX, x.Data);
        }
        [TestMethod] public async Task UpdateAsyncTest() {
            await GetTest();
            var dX = GetRandom.Value<StudentData>() as StudentData;
            isNotNull(d);
            isNotNull(dX);
            dX.ID = d.ID;
            var aX = new Student(dX);
            _ = await obj.UpdateAsync(aX);
            var x = obj.Get(d.ID);
            areEqualProperties(dX, x.Data);
        }
      
    }
}

