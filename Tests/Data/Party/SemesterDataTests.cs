using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Tests.Data.Party
{
    [TestClass]
    public class SemesterDataTests : SealedClassTests<SemesterData,UniqueData>
    {
        [TestMethod] public void SeasonTest() => isProperty<IsoSemester?>();
        [TestMethod] public void DateStartTest() => isProperty<DateTime?>();
        [TestMethod] public void DateEndTest() => isProperty<DateTime?>();

    }
}
