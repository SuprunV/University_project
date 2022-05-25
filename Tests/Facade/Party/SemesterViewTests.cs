using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.IsoEnums;
using Project_1.Facade;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party
{
    [TestClass]
    public class SemesterViewTests : SealedClassTests<SemesterView, UniqueView> {
        [TestMethod] public void SeasonTest() => isRequired<IsoSemester?>("Season");
        [TestMethod] public void SeasonFullTest() => isDisplayNamed<string?>("Info");
        [TestMethod] public void DateStartTest() => isRequired<DateTime?>("Start");
        [TestMethod] public void DateEndTest() => isRequired<DateTime?>("End");
    }
}
