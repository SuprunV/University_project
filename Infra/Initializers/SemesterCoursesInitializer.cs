using Project_1.Data.Connection;

namespace Project_1.Infra.Initializers {
    public sealed class SemesterCoursesInitializer : BaseInitializer<SemesterCourseData> {
        public SemesterCoursesInitializer(UniversityDb? db) : base(db, db?.SemestersCourse) { }
        protected override IEnumerable<SemesterCourseData> getEntities => new[] {
            //Here values add
            createSemesterCourses("Course 26","Autumn2023"),
            createSemesterCourses("Course 62","Autumn2023"),
            createSemesterCourses("Course 87","Autumn2023"),
            createSemesterCourses("Course 44","Autumn2023"),
            createSemesterCourses("Course 11","Autumn2023"),
            createSemesterCourses("Course 112","Spring2023"),
            createSemesterCourses("Course 910","Spring2023"),
            createSemesterCourses("Course 75","Spring2023"),
            createSemesterCourses("Course 51","Spring2023"),
            createSemesterCourses("Course 33","Spring2023")

        };
        internal static SemesterCourseData createSemesterCourses(string couID, string semID)
            => new SemesterCourseData() {
                ID = Guid.NewGuid().ToString(),
                SemesterID = semID,
                CourseID = couID
            };
    }
}
