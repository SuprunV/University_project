using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade.Party;

namespace Project_1.Tests.Facade.Party {
    [TestClass]
    public class StudyProgramViewFactoryTests : ViewFactoryTests
        <StudyProgramViewFactory, StudyProgramView, StudyProgram, StudyProgramData>
    {
        protected override StudyProgram toObject(StudyProgramData d) => new(d);
    }
}
