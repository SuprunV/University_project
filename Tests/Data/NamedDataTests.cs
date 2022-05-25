
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.IsoEnums;

namespace Project_1.Tests.Data
{
    [TestClass]
    public class NamedDataTests : AbstractClassTests<NamedData, UniqueData>
    {
        private class TestClass : NamedData { }
        protected override NamedData createObj() => new TestClass();
        [TestMethod] public void UniIDTest() => isProperty<string>();
        [TestMethod] public void LastNameTest() => isProperty<string>();
        [TestMethod] public void FirstNameTest() => isProperty<string?>();
        [TestMethod] public void SexTest() => isProperty<IsoGender?>();
    }
}
