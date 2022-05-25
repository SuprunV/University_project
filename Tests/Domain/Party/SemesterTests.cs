using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass] public class SemesterTests : SealedClassTests<Semester, UniqueEntity<SemesterData>>
    {
        protected override Semester createObj() => new(GetRandom.Value<SemesterData>());
        [TestMethod] public void SeasonTest() => isReadOnly(obj.Data.Season);
        [TestMethod] public void DateStartTest() => isReadOnly(obj.Data.DateStart);
        [TestMethod] public void DateEndTest() => isReadOnly(obj.Data.DateEnd);
        [TestMethod]
        public void SeasonFullTest()
        {
            var expected = $"{obj.Season} {obj.DateStart.Year}";
            areEqual(expected, obj.SeasonFull());
        }


    }
}
