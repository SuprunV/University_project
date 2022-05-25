using Project_1.Data.Party;
using Project_1.Domain.Connection;

namespace Project_1.Domain.Party
{
    public sealed class Course : UniqueEntity<CourseData>
    {
        public Course() : this(new CourseData()) { }
        public Course(CourseData d):base(d){}

        public string? CourseTitle => getValue(Data?.CourseTitle);
        public int EAP => getValue(Data?.EAP);
        public string? Description => getValue(Data?.Description);
        public string? OwnerID => getValue(Data?.OwnerID);


        public string? CreateTotalString() => $"{CourseTitle} ({EAP} EAP, Owner of course: {(lecturerID == "Undefined Undefined" ? "Undefined" : lecturerID)})";

        public Lecturer? lecturer => GetRepo.Instance<ILecturersRepo>()?.Get(OwnerID ?? String.Empty);
        public string? lecturerID => GetRepo.Instance<ILecturersRepo>()?.GetBy(x => x.ID == OwnerID)?.FullName ?? "";
        public string? studyProgramID => GetRepo.Instance<IStudyProgramsCoursesRepo>()?.GetBy(x => x.CourseID == ID)?.StudyProgramID ?? "";
        public string? semesterID => GetRepo.Instance<ISemesterCourseRepo>()?.GetBy(x => x.CourseID == ID)?.SemesterID ?? "";

    }
    public interface ICoursesRepo : IRepo<Course> {
        Task<bool> UpdateNeededEntities(Course main, SemesterCourse sc, StudyProgramCourse spc);
    }
    public interface IJoinedCoursesRepo : IRepo<Course> { }
    public interface IMyCoursesRepo : IRepo<Course> { }
}
