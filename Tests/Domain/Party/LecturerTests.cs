using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Aids;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;

namespace Project_1.Tests.Domain.Party
{
    [TestClass] public class LecturerTests : SealedClassTests<Lecturer, NamedEntity<LecturerData>>
    {
        protected override Lecturer createObj() => new(GetRandom.Value<LecturerData>());
    }

}
