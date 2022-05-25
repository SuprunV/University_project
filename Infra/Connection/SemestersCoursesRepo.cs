using Project_1.Data.Connection;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;
using Project_1.Infra.Party;
using Project_1.Infra.Party.Courses;

namespace Project_1.Infra.Connection
{
    public sealed class SemestersCoursesRepo:Repo<SemesterCourse,SemesterCourseData>, ISemesterCourseRepo
    {
        public readonly SemestersRepo? sr;
        public readonly List<Semester>? semesters;
        public readonly CoursesRepo? cr;
        public readonly List<Course>? courses;
        public SemestersCoursesRepo(UniversityDb? db) : base(db, db?.SemestersCourse) { }
        
        protected internal override SemesterCourse toDomain(SemesterCourseData d) => new(d);

        internal override IQueryable<SemesterCourseData> addFilter(IQueryable<SemesterCourseData> q)
        {
            return q;
        }
    }
}
    

   

