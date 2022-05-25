using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;

namespace Project_1.Tests.Domain {
    [TestClass] public class NamedEntityTests : AbstractClassTests<NamedEntity<LecturerData>, UniqueEntity<LecturerData>> {
        private class testClass : NamedEntity<LecturerData> {
            public testClass() : this(new LecturerData()) { }
            public testClass(LecturerData d) : base(d) { }
        }
        protected override NamedEntity<LecturerData> createObj() => new testClass(GetRandom.Value<LecturerData>());
        [TestMethod] public void UniIDTest() => isReadOnly(obj.Data.UniID);
        [TestMethod] public void FirstNameTest() => isReadOnly(obj.Data.FirstName);
        [TestMethod] public void LastNameTest() => isReadOnly(obj.Data.LastName);
        [TestMethod] public void SexTest() => isReadOnly(obj.Data.Sex);
        [TestMethod] public void FullNameTest() => isReadOnly(obj.CreateTotalString());
        [TestMethod] public void CreateTotalStringTest()
        {
            var expected = $"{obj.FirstName} {obj.LastName}";
            areEqual(expected, obj.CreateTotalString());
        }


    }
}
