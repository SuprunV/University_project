using Project_1.Data.Connection;


namespace Project_1.Infra.Initializers {
    public sealed class StudyProgramCoursesInitializer : BaseInitializer<StudyProgramCourseData> {
        public StudyProgramCoursesInitializer(UniversityDb? db) : base(db, db?.StudyProgramsCourses) { }
        protected override IEnumerable<StudyProgramCourseData> getEntities => new[] {
            //Here values add
            createStudyProgramCourses("Course 26","Econom"),
            createStudyProgramCourses("Course 62","Econom"),
            createStudyProgramCourses("Course 87","Inform"),
            createStudyProgramCourses("Course 44","Inform"),
            createStudyProgramCourses("Course 11","Inform"),
            createStudyProgramCourses("Course 112","Philos"),
            createStudyProgramCourses("Course 910","Philos"),
            createStudyProgramCourses("Course 75","Scienc"),
            createStudyProgramCourses("Course 51","Scienc"),
            createStudyProgramCourses("Course 33","Scienc")
        };
        internal static StudyProgramCourseData createStudyProgramCourses(string couID, string studyPrID)
            => new StudyProgramCourseData() {
                ID = Guid.NewGuid().ToString(),
                CourseID = couID,
                StudyProgramID = studyPrID
            };
    }
}
