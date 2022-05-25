using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Infra;
using Project_1.Infra.Connection;

namespace Project_1.Tests.Infra.Connection
{
    [TestClass]
    public class CourseLecturersRepoTests
        : SealedRepoTests<CourseLecturersRepo, Repo<CourseLecturer, CourseLecturerData>, CourseLecturer,
            CourseLecturerData>
    {
        protected override CourseLecturersRepo createObj() => new(GetRepo.Instance<UniversityDb>() ?? new UniversityDb(new DbContextOptions<UniversityDb>()));
        protected override object? getSet(UniversityDb? db) => db?.CourseLecturers;

    }
}
