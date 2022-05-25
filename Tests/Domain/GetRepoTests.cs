using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra.Party;

namespace Project_1.Tests.Domain
{
    [TestClass] public class GetRepoTests : TypeTests {
        private class TestClass : IServiceProvider {
            public object? GetService(Type serviceType)
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod] public void InstanceTest() 
            => Assert.IsInstanceOfType(GetRepo.Instance<ICountriesRepo>(), typeof(CountriesRepo));
        [TestMethod] public void SetServiceTest() {
            var s = GetRepo.service;
            var x = new TestClass();
            GetRepo.SetService(x);
            areEqual(x, GetRepo.service);
            GetRepo.service = s;
        }
    }
}
