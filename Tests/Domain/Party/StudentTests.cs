using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass]
    public class StudentTests : SealedClassTests<Student, NamedEntity<StudentData>>
    {
        protected override Student createObj() => new(GetRandom.Value<StudentData>());
        [TestMethod] public void NationalityTest() => isReadOnly(obj.Data.Nationality);
        [TestMethod] public void EnrollmentDateTest() => isReadOnly(obj.Data.EnrollmentDate);
        [TestMethod] public void StudyProgramIDTest() => isReadOnly(obj.Data.StudyProgramID);
    }
}