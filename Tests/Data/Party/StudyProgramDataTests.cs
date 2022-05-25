using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Party;
using Project_1.Data;

namespace Project_1.Tests.Data.Party {

    [TestClass]
    public class StudyProgramDataTests : SealedClassTests<StudyProgramData,UniqueData> {
        [TestMethod] public void StudyProgramsTitleTest() => isProperty<string?>();
        [TestMethod] public void DurationTest() => isProperty<int?>();
        [TestMethod] public void StudyProgramsDescriptionTest() => isProperty<string?>();
    }
}
