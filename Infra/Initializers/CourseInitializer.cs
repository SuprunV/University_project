using Project_1.Data.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Infra.Initializers {
    public sealed class CourseInitializer : BaseInitializer<CourseData> {
        public CourseInitializer(UniversityDb? db) : base(db, db?.Courses) { }
        protected override IEnumerable<CourseData> getEntities => new[] {
            //Here values add
            createCourse("Course 1", 12, "description 1", "MillerReijo"),
            createCourse("Course 2", 6,  "description 2", "PajusaarHeli"),
            createCourse("Course 3", 3,  "description 3", "LepiksooErna"),
            createCourse("Course 4", 4, "description 4", "KoortMeelis"),
            createCourse("Course 5", 1,  null, "VeesimaaAarne"),
            createCourse("Course 6", 2,  null, "MillerReijo"),
            createCourse("Course 7", 5,  null, "MillerReijo"),
            createCourse("Course 8", 7,  null, "PajusaarHeli"),
            createCourse("Course 9", 10, null, "PajusaarHeli"),
            createCourse("Course 10", 1,  null, "KoortMeelis")
        };
        internal static CourseData createCourse(string title, int eap, string? descr, string ownerID)
            => new CourseData {
                   ID = title.Substring(0, (title.Length < 8) ? title.Length : 8)+eap,
                   CourseTitle = title,
                   EAP = eap,
                   OwnerID= ownerID,
                   
                Description = descr
            };
    }
}
