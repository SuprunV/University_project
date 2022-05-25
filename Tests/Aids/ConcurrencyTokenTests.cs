using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;

namespace Project_1.Tests.Aids
{
    [TestClass]
    public class ConcurrencyTokenTests:TypeTests
    {
        [TestMethod]
        public void ToStrTest()
        {
            Random rnd = new Random();
            var bytes = new Byte[rnd.Next(1,5)];
            var s = string.Empty;
            foreach (var b in bytes ?? Array.Empty<byte>()) s += b; 
            areEqual(s,ConcurrencyToken.ToStr(bytes));
        }
        [TestMethod]
        public void ToByteArrayTest()
        {
            string text = GetRandom.String(5,10);
            var result = new List<byte>();
            foreach (var c in text) result.Add(Convert.ToByte(c));
            areEqual(result.ToArray().ToString(), ConcurrencyToken.ToByteArray(text).ToString());

        }
    }
}
