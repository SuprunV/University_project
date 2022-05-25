using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_1.Data.Connection;
using Project_1.Domain;
using Project_1.Domain.Connection;
using Project_1.Infra;
using Project_1.Infra.Connection;

namespace Project_1.Tests.Infra.Connection
{
    [TestClass]
    public class EnrollmentsRepoTests
        : SealedRepoTests<EnrollmentsRepo, Repo<Enrollment, EnrollmentData>, Enrollment, EnrollmentData>
    {
        protected override EnrollmentsRepo createObj() => new(GetRepo.Instance<UniversityDb>());
        protected override object? getSet(UniversityDb db) => db.Enrollments;
    }

    
}
