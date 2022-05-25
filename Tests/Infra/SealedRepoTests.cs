using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data;
using Project_1.Domain;
using Project_1.Infra;

namespace Project_1.Tests.Infra {
    public abstract class SealedRepoTests<TClass, TBaseClass, TDomain, TData>
        : SealedBaseTests<TClass, TBaseClass>
        where TClass : FilteredRepo<TDomain, TData>
        where TBaseClass : class
        where TDomain : UniqueEntity<TData>, new()
        where TData : UniqueData, new() {
        private static Type universityType => typeof(UniversityDb);
        protected List<string?> itemsToBeFiltered = new List<string?>();
        private static Type setType => typeof(DbSet<TData>);
        private UniversityDb universityDb {
            get {
                var o = obj.db;
                isNotNull(o);
                var db = o as UniversityDb;
                isNotNull(db);
                return db;
            }
        }
        protected void instanceTest(object? o, Type t) {
            isNotNull(o);
            isInstanceOfType(o, t);
        }
        protected void instanceTest(object? o, Type t, object? expected) {
            instanceTest(o, t);
            instanceTest(expected, t);
            areEqual(expected, o);
        }
        [TestMethod] public void DbContextTest() => instanceTest(obj.db, universityType);
        [TestMethod] public void DbSetTest() => instanceTest(obj.set, setType, getSet(universityDb) );
        [TestMethod] public void ToDomainTest() {
            var d = GetRandom.Value<TData>();
            var o = obj.toDomain(d);
            isNotNull(o);
            isInstanceOfType(o, typeof(TDomain));
            areEqualProperties(d, o.Data);
        }

        protected abstract object? getSet(UniversityDb db);
    }
}
