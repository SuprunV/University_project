using Project_1.Data.Connection;
using Project_1.Domain.Party;

namespace Project_1.Domain.Connection
{
    public interface ISemesterCourseRepo : IRepo<SemesterCourse> { }
    public sealed class SemesterCourse:UniqueEntity<SemesterCourseData>
    {
        public SemesterCourse() : this(new()) { }
        public SemesterCourse(SemesterCourseData d):base(d){}
        public string SemesterID => getValue(Data?.SemesterID);
        public string CourseID => getValue(Data?.CourseID);
        public Course? Course => GetRepo.Instance<ICoursesRepo>()?.Get(CourseID);
        public Semester? Semester => GetRepo.Instance<ISemestersRepo>()?.Get(SemesterID);
    }
}
