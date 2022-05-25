using Project_1.Data.Connection;
using Project_1.Domain.Connection;

namespace Project_1.Infra.Connection {
    public sealed class CourseLecturersRepo : Repo<CourseLecturer, CourseLecturerData>, ICourseLecturerRepo {
        public CourseLecturersRepo(UniversityDb db) : base(db, db?.CourseLecturers) { }
        protected internal override CourseLecturer toDomain(CourseLecturerData d) => new(d);
        internal override IQueryable<CourseLecturerData> addFilter(IQueryable<CourseLecturerData> q) {
            var y = CurrentFilter;
            return string.IsNullOrWhiteSpace(y) ?
                q : q.Where(x =>
                        x.LecturerID.Contains(y) ||
                        x.CourseID.Contains(y)
                    );
            ;

        }
    }
}
