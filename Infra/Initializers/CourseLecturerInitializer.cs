using Project_1.Data.Connection;
using Project_1.Data.IsoEnums;
using Project_1.Data.Party;

namespace Project_1.Infra.Initializers {
    public sealed class CourseLecturerInitializer : BaseInitializer<CourseLecturerData> {
        public CourseLecturerInitializer(UniversityDb? db) : base(db, db?.CourseLecturers) { }
        protected override IEnumerable<CourseLecturerData> getEntities => new[] {
            //Here values add
            createCourseLecturer("Course 62","LepiksooErna"),
            createCourseLecturer("Course 112","MillerReijo"),
            createCourseLecturer("Course 33","LepiksooErna"),
            createCourseLecturer("Course 51","MillerReijo"),
            createCourseLecturer("Course 44","VeesimaaAarne"),
            createCourseLecturer("Course 910","PajusaarHeli"),
            createCourseLecturer("Course 44","KoortMeelis"),
            createCourseLecturer("Course 11","KoortMeelis"),
            createCourseLecturer("Course 51","VeesimaaAarne"),
            createCourseLecturer("Course 11","LepiksooErna"),
            createCourseLecturer("Course 87","PajusaarHeli"),
            createCourseLecturer("Course 26","PajusaarHeli"),
            createCourseLecturer("Course 75","MillerReijo")
        };
        internal static CourseLecturerData createCourseLecturer(string courseID, string LecturerID)
            => new CourseLecturerData {
                ID = Guid.NewGuid().ToString(),
                CourseID = courseID,
                LecturerID = LecturerID
            };
    }
}
