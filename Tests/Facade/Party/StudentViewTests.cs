using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Facade;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party
{
    [TestClass]
    public class StudentViewTests : SealedClassTests<StudentView,NamedView> {
        [TestMethod] public void NationalityTest() => isRequired<string?>("Nationality");
        [TestMethod] public void EnrollmentDateTest() => isRequired<DateTime?>("Date of enrollment");
        [TestMethod] public void StudyProgramIDTest() => isRequired<string?>("Study program");
    }
}
