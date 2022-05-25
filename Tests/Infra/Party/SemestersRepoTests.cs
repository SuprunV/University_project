using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;
using Project_1.Infra.Party;

namespace Project_1.Tests.Infra.Party {
    [TestClass] public class SemestersRepoTests 
        : SealedRepoTests<SemestersRepo, Repo<Semester, SemesterData>, Semester, SemesterData> {
       
        protected override SemestersRepo createObj() => new (GetRepo.Instance<UniversityDb>());
        protected override object? getSet(UniversityDb db) => db.Semesters;
    }
}
