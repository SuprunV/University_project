using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;

namespace Project_1.Tests.Domain {
    [TestClass]  public class UniqueEntityTests : AbstractClassTests<UniqueEntity<CountryData>, UniqueEntity > {
        private CountryData? d ;
        private class testClass : UniqueEntity<CountryData> { 
            public testClass() : this(new CountryData()) { }
            public testClass(CountryData? d) : base(d!) { }
        }
        protected override UniqueEntity<CountryData> createObj() {
            d = GetRandom.Value<CountryData>();
            return new testClass(d);
        }
        [TestMethod] public void IDTest() => isReadOnly(obj.Data.ID);
        [TestMethod] public void TokenTest() => isReadOnly(obj.Data.Token);
        [TestMethod] public void DataTest() => isReadOnly(d);
        [TestMethod] public void defaultStrTest() => areEqual("Undefined", UniqueEntity.defaultStr);
        [TestMethod] public void defaultDateTest() => areEqual("01.01.0001 00:00:00", UniqueEntity.defaultDate.ToString());
        [TestMethod] public void defaultBoolTest() => areEqual(false, UniqueEntity.defaultBool);
        [TestMethod] public void defaultIntTest() => areEqual(0, UniqueEntity.defaultInt);
    }
}
