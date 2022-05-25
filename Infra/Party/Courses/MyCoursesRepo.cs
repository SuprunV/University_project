
using Microsoft.EntityFrameworkCore;
using Project_1.Data.Party;
using Project_1.Domain.Party;


namespace Project_1.Infra.Party.Courses
{
    public sealed class MyCoursesRepo : Repo<Course, CourseData>, IMyCoursesRepo
    {
        private UniversityDb? dbSet;
        public MyCoursesRepo(UniversityDb? c) : base(c, c?.Courses) { dbSet = c; }
        protected internal override Course toDomain(CourseData d) => new(d);
        internal override IQueryable<CourseData> addFilter(IQueryable<CourseData> q) => FilteringCourse.addFilter(q, dbSet, CurrentFilter, StudyProgramFilter, SemesterFilter, UserID);
        internal override IQueryable<CourseData> addAdvancedFilter(IQueryable<CourseData> q) => q.Where(i => i.OwnerID == SessionUserID);
    
    }
}
