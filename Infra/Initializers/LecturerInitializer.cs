using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Infra.Initializers {
    public sealed class LecturerInitializer : BaseInitializer<LecturerData> {
        public LecturerInitializer(UniversityDb? db) : base(db, db?.Lecturers) { }
        protected override IEnumerable<LecturerData> getEntities => new[] {
            //Here values add 
            createLecturer("Reijo", "Miller" , IsoGender.Female),
            createLecturer("Heli", "Pajusaar" , IsoGender.Male),
            createLecturer("Erna", "Lepiksoo" , IsoGender.NotApplicable),
            createLecturer("Meelis", "Koort" , IsoGender.Male),
            createLecturer("Aarne", "Veesimaa" , IsoGender.NotKnown)
        };

        internal static LecturerData createLecturer(string lastName, string firstName, IsoGender sex)
            => new LecturerData {
                ID = firstName+lastName,
                UniID = firstName+lastName,
                LastName = lastName,
                FirstName = firstName,
                Sex = sex   
            };
    }
}
