using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Facade;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party
{
    [TestClass]
    public class StudyProgramViewTests : SealedClassTests<StudyProgramView,UniqueView> {
        [TestMethod] public void StudyProgramsTitleTest() => isRequired<string?>("Study program title");
        [TestMethod] public void DurationTest() => isRequired<int?>("Program duration: (in years)");
        [TestMethod] public void StudyProgramsDescriptionTest() => isDisplayNamed<string?>("Description");
    }
}
