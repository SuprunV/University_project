using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Facade;

namespace Project_1.Tests.Facade
{
    [TestClass] public class UniqueViewTests : AbstractClassTests<UniqueView, object> {
        private class TestClass : UniqueView { }
        protected override UniqueView createObj() => new TestClass();
        [TestMethod] public void IDTest() => isRequired<string>("ID");
        [TestMethod] public void TokenTest() => isRequired<byte[]>();
    }
}
