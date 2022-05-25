using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain.Connection;
using Project_1.Domain.Party;

namespace Project_1.Infra.Party.Courses
{
    public sealed class CoursesRepo: Repo<Course, CourseData>, ICoursesRepo
    {
        private readonly UniversityDb? dbSet;
        public CoursesRepo(UniversityDb? db) : base(db, db?.Courses) {
            dbSet = db ?? throw new ArgumentNullException(nameof(db)) ;
        }
        protected internal override Course toDomain(CourseData d) => new (d);
        internal override IQueryable<CourseData> addFilter(IQueryable<CourseData> q) => FilteringCourse.addFilter(q, dbSet , CurrentFilter, StudyProgramFilter, SemesterFilter, UserID);
        private static void Copy(object? from, object? to)
        {
            var tFrom = from?.GetType();
            var tTo = to?.GetType();
            foreach (var piFrom in tFrom?.GetProperties() ?? Array.Empty<PropertyInfo>())
            {
                var v = piFrom.GetValue(from, null);
                var piTo = tTo?.GetProperty(piFrom.Name);
                piTo?.SetValue(to, v, null);
            }
        }
        public async Task<bool> UpdateNeededEntities(Course main, SemesterCourse sc, StudyProgramCourse spc) {
            var entityEntries = dbSet?.ChangeTracker.Entries().Where(t => t.State == EntityState.Modified);
            try {
                if (dbSet is null) return false;

                var d1 = main.Data;
                var d2 = sc.Data;
                var d3 = spc.Data;

                var d1Data = dbSet?.Courses?.FirstOrDefault(u => u.ID == d1.ID) ;
                var d2Data = dbSet?.SemestersCourse?.FirstOrDefault(u => u.ID == d2.ID);
                var d3Data = dbSet?.StudyProgramsCourses?.FirstOrDefault(u => u.ID == d3.ID);


                Copy(d1, d1Data);
                Copy(d2, d2Data);
                Copy(d3, d3Data);

                dbSet?.Courses?.Update(d1Data ?? new CourseData());
                dbSet?.SemestersCourse?.Update(d2Data ?? new SemesterCourseData());
                dbSet?.StudyProgramsCourses?.Update(d3Data ?? new StudyProgramCourseData());

               _ = await dbSet.SaveChangesAsync();
               return true;
            }
            catch {
                return false;
            }
        }
    }
}
    