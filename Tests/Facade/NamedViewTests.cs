using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.IsoEnums;
using Project_1.Facade;

namespace Project_1.Tests.Facade {
    [TestClass] public class NamedViewTests : AbstractClassTests<NamedView, UniqueView> {
        private class TestClass : NamedView { }
        protected override NamedView createObj() => new TestClass();
        [TestMethod] public void UniIDTest() => isRequired<string?>("Uni ID");
        [TestMethod] public void LastNameTest() => isRequired<string?>("Last name");
        [TestMethod] public void FirstNameTest() => isDisplayNamed<string?>("First name");
        [TestMethod] public void SexTest() => isDisplayNamed<IsoGender?>("Sex");
        [TestMethod] public void FullNameTest() => isDisplayNamed<string?>("Full name");
    }
}
