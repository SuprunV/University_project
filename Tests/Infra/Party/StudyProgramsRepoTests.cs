using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Party;
using Project_1.Domain;
using Project_1.Domain.Party;
using Project_1.Infra;
using Project_1.Infra.Party;

namespace Project_1.Tests.Infra.Party {
    [TestClass] public class StudyProgramsRepoTests
        : SealedRepoTests<StudyProgramsRepo, Repo<StudyProgram, StudyProgramData>, StudyProgram, StudyProgramData> {

        protected override StudyProgramsRepo createObj() => new (GetRepo.Instance<UniversityDb>());
        protected override object? getSet(UniversityDb db) => db.StudyPrograms;
    }
}
