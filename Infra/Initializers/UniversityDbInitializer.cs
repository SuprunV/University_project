namespace Project_1.Infra.Initializers;

public static  class UniversityDbInitializer
{
    public static void Init(UniversityDb? db)
    {
        new CourseInitializer(db).Init();
        new StudentInitializer(db).Init();
        new LecturerInitializer(db).Init();
        new SemesterInitializer(db).Init();
        new StudyProgramInitializer(db).Init();
        new CountryInitializer(db).Init();
        new CourseLecturerInitializer(db).Init();
        new SemesterCoursesInitializer(db).Init();
        new StudyProgramCoursesInitializer(db).Init();
    }
}