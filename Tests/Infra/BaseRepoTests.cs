using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;

namespace Project_1.Tests.Infra {
    [TestClass] public class BaseRepoTests 
        : AbstractClassTests<BaseRepo<Student, StudentData>, SessionRepo> {
        private class testClass : BaseRepo<Student, StudentData> {
            public testClass(DbContext? c, DbSet<StudentData>? s) : base(c, s) { }
            public override bool Add(Student obj) => throw new NotImplementedException();
            public override Task<bool> AddAsync(Student obj) => throw new NotImplementedException();
            public override bool Delete(string id) => throw new NotImplementedException();
            public override Task<bool> DeleteAsync(string id) => throw new NotImplementedException();
            public override List<Student> Get() => throw new NotImplementedException();
            public override Student GetBy(Func<Student, bool>? condition) => throw new NotImplementedException();
            public override List<Student> GetAll(Func<Student, dynamic>? orderBy = null) => throw new NotImplementedException();
            public override Student Get(string id) => throw new NotImplementedException();
            public override Task<List<Student>> GetAsync() => throw new NotImplementedException();
            public override Task<Student> GetAsync(string id) => throw new NotImplementedException();
            public override bool Update(Student obj) => throw new NotImplementedException();
            public override Task<bool> UpdateAsync(Student obj) => throw new NotImplementedException();
        }
        protected override BaseRepo<Student, StudentData> createObj() => new testClass(null, null);
        [TestMethod] public void dbTest() => isReadOnly<DbContext?>();
        [TestMethod] public void setTest() => isReadOnly<DbSet<StudentData>?>();
        [TestMethod] public void BaseRepoTest() {
            var db = GetRepo.Instance<UniversityDb>();
            var set = db?.Students;
            isNotNull(set);
            obj = new testClass(db, set);
            areEqual(db, obj.db);
            areEqual(set, obj.set);
        }
        [TestMethod] public async Task ClearTest() {
            BaseRepoTest();
            var cnt = GetRandom.Int32(5, 30);
            var db = obj.db;
            isNotNull(db);
            var set = obj.set;
            isNotNull(set);
            for (var i = 0; i < cnt; i++) set.Add(GetRandom.Value<StudentData>());
            areEqual(0, await set.CountAsync());
            db.SaveChanges();
            areEqual(cnt, await set.CountAsync());
            obj.clear();
            areEqual(0, await set.CountAsync());
        }
        [TestMethod] public void AddTest() => isAbstractMethod(nameof(obj.Add), typeof(Student));
        [TestMethod] public void AddAsyncTest() => isAbstractMethod(nameof(obj.AddAsync), typeof(Student));
        [TestMethod] public void DeleteTest() => isAbstractMethod(nameof(obj.Delete), typeof(string));
        [TestMethod] public void DeleteAsyncTest() => isAbstractMethod(nameof(obj.DeleteAsync), typeof(string));
        [TestMethod] public void GetTest() => isAbstractMethod(nameof(obj.Get), typeof(string));
        [TestMethod] public void GetAllTest() => isAbstractMethod(nameof(obj.GetAll), typeof(Func<Student, dynamic>));
        [TestMethod] public void GetListTest() => isAbstractMethod(nameof(obj.Get));
        [TestMethod] public void GetAsyncTest() => isAbstractMethod(nameof(obj.GetAsync), typeof(string));
        [TestMethod] public void GetListAsyncTest() => isAbstractMethod(nameof(obj.GetAsync));
        [TestMethod] public void UpdateTest() => isAbstractMethod(nameof(obj.Update), typeof(Student));
        [TestMethod] public void UpdateAsyncTest() => isAbstractMethod(nameof(obj.UpdateAsync), typeof(Student));
        [TestMethod] public void GetByTest() => isAbstractMethod(nameof(obj.GetBy), typeof(Func<Student,bool>));
    }
}

