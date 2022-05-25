using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass] public class StudyProgramTests : SealedClassTests<StudyProgram, UniqueEntity<StudyProgramData>>
    {
        protected override StudyProgram createObj() => new(GetRandom.Value<StudyProgramData>());
        [TestMethod] public void StudyProgramsTitleTest() => isReadOnly(obj.Data.StudyProgramsTitle);
        [TestMethod] public void StudyProgramsDescriptionTest() => isReadOnly(obj.Data.StudyProgramsDescription);
        [TestMethod] public void DurationTest() => isReadOnly(obj.Duration);
    }
}
