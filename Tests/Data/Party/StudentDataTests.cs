using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Tests.Data.Party
{
    [TestClass]
    public class StudentDataTests : SealedClassTests<StudentData,NamedData> {
        [TestMethod] public void NationalityTest() => isProperty<string?>();
        [TestMethod] public void EnrollmentDateTest() => isProperty<DateTime?>();
        [TestMethod] public void StudyProgramIDTest() => isProperty<string?>();
    }
}
