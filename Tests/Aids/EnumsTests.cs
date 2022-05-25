using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.IsoEnums;

namespace Project_1.Tests.Aids
{
    [TestClass] public class EnumsTests : TypeTests {
        [TestMethod]
        public void DescriptionTest()
        {
            areEqual("Not applicable", Enums.Description(IsoGender.NotApplicable)); 
            areEqual("Not applicable", Enums.Description(IsoSemester.NotApplicable));
        }

        [TestMethod]
       public void ToStringTest()
       {
           areEqual("NotApplicable", IsoGender.NotApplicable.ToString());
           areEqual("NotApplicable", IsoSemester.NotApplicable.ToString());
       }

   }
}
