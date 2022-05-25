using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Infra.Initializers {
    public sealed class StudentInitializer : BaseInitializer<StudentData> {
        public StudentInitializer(UniversityDb? db) : base(db, db?.Students) { }
        protected override IEnumerable<StudentData> getEntities => new[] {
            //Here values add
            createStudent( "Salumets", "Laine", IsoGender.Male, new DateTime(2001,11,30), null,"Econom"),
            createStudent( "Meister", "Malle", IsoGender.Female, new DateTime(1999,09,28), "EST","Econom"),
            createStudent( "Rajamäe", "Hiiu", IsoGender.NotApplicable, new DateTime(2000,12,15), "LTU","Inform"),
            createStudent( "Berk", "Ene", IsoGender.Female, new DateTime(1998,05,19), "USA","Philos"),
            createStudent( "Astok", "Margus", IsoGender.NotKnown, new DateTime(1990,07,22), "LVA","Scienc"),
            createStudent( "Oliver", "Puu", IsoGender.Male, new DateTime(2000,07,22), "LVA","Scienc"),
            createStudent( "Emily", "Piir", IsoGender.Female, new DateTime(2000,07,22), "EST","Inform"),
            createStudent( "James", "Bond", IsoGender.Male, new DateTime(2000,07,22), "EST","Inform"),
            createStudent( "Noah", "Open", IsoGender.Male, new DateTime(1993,07,22), "EST","Inform"),
        };
        internal static StudentData createStudent(string lastName, string firstName, IsoGender sex, DateTime enrollmentDate,
            string? nationality, string studyprId)
           => new StudentData {
               ID = lastName+firstName.Substring(0, (firstName.Length < 3) ? firstName.Length : 3),
               UniID = lastName+firstName,
               LastName = lastName,
               FirstName = firstName,
               EnrollmentDate = enrollmentDate,
               Sex = sex,
               Nationality = nationality,
               StudyProgramID = studyprId
           };
    }
}
