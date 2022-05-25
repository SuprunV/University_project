using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Infra.Initializers {
    public sealed class SemesterInitializer : BaseInitializer<SemesterData> {
        public SemesterInitializer(UniversityDb? db) : base(db, db?.Semesters) { }
        protected override IEnumerable<SemesterData> getEntities => new[] {
            //Here values add
            createSemester(IsoSemester.Autumn, new DateTime(2022, 09,01), new DateTime(2023, 01,13)),
            createSemester(IsoSemester.Spring, new DateTime(2023, 02,01), new DateTime(2023, 06,17)),
            createSemester(IsoSemester.Autumn, new DateTime(2023, 09,01), new DateTime(2023, 01,13)),
            createSemester(IsoSemester.Spring, new DateTime(2024, 02,01), new DateTime(2023, 06,17))

        };
        internal static SemesterData createSemester(IsoSemester season, DateTime start, DateTime end)
            => new SemesterData {
                ID = season.ToString() + start.Year,
                Season = season,
                DateStart = start,
                DateEnd = end
            };
    }
}
