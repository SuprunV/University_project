
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;

namespace Project_1.Tests.Aids {
    [TestClass]
    public class GetNamespaceTests : TypeTests
    {
        [TestMethod] public void OfTypeTest()
        {
            var obj = new CountryData();
            var name = obj.GetType().Namespace;
            var n = GetNamespace.OfType(obj);
            areEqual(name, n);
        }
        [TestMethod] public void OfTypeNullTest()
        {
            CountryData? obj = null;
            var n = GetNamespace.OfType(obj) ;
            areEqual(string.Empty, n);
            
        }

    }


}