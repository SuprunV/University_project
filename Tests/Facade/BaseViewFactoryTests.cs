using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain.Party;
using Project_1.Facade;
using Project_1.Facade.Party;

//How to controll not only Student
namespace Project_1.Tests.Facade
{
    [TestClass]
    public class BaseViewFactoryTests : AbstractClassTests<
        BaseViewFactory<StudyProgramView, StudyProgram, StudyProgramData>, object>
    {
        private class TestClass : BaseViewFactory<StudyProgramView, StudyProgram, StudyProgramData>
        {
            protected override StudyProgram toEntity(StudyProgramData d) => new StudyProgram(d);
        }

        protected override BaseViewFactory<StudyProgramView, StudyProgram, StudyProgramData> createObj() =>
            new TestClass();

        [TestMethod] public void CreateTest() { }

        [TestMethod] public void CreateViewTest()
        {
            var v = GetRandom.Value<StudyProgramView>();
            var o = obj.Create(v);
            areEqualProperties(v, o.Data);
        }

        [TestMethod] public void CreateObjectTest()
        {
            var d = GetRandom.Value<StudyProgramData>();
            var v = obj.Create(new StudyProgram(d));
            areEqualProperties(d, v);
        }

        [TestMethod] public void CreateUndefinedTest()
        {
            var v = GetRandom.Value<StudyProgramView>();
            var o = obj.Create(v);
            areEqualProperties(v, o);
        }
    }
}
