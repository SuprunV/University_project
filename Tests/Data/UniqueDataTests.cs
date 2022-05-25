using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;

namespace Project_1.Tests.Data
{
    [TestClass]
    public class UniqueDataTests : AbstractClassTests<UniqueData, object>
    {
        private class TestClass : UniqueData { }
        protected override UniqueData createObj() => new TestClass();
        [TestMethod]
        public void NewIDTest()
        {
            isNotNull(UniqueData.NewID);
            areNotEqual(UniqueData.NewID, UniqueData.NewID);
            var pi = typeof(UniqueData).GetProperty(nameof(UniqueData.NewID));
            isFalse(pi?.CanWrite);
        }
        [TestMethod] public void IDTest() => isProperty<string>();
        [TestMethod] public void TokenTest() => isProperty<byte[]>();
    }
}
