using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.Party;

namespace Project_1.Tests.Data.Party
{
    [TestClass]
    public class CountryDataTests : SealedClassTests<CountryData, UniqueData>
    {
        [TestMethod] public void ISOTest() => isProperty<string?>();
        [TestMethod] public void CountryNameTest() => isProperty<string?>();
        [TestMethod] public void NativeNameTest() => isProperty<string?>();
    }
}
