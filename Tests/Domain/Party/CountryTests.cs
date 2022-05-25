using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass] public class CountryTests : SealedClassTests<Country, UniqueEntity<CountryData>>
    {
        protected override Country createObj() => new(GetRandom.Value<CountryData>());
        [TestMethod] public void CountryNameTest() => isReadOnly(obj.Data.CountryName);
        [TestMethod] public void ISOTest() => isReadOnly(obj.Data.ISO);
        [TestMethod] public void NativeNameTest() => isReadOnly(obj.Data.NativeName);
    }
}
