using Microsoft.EntityFrameworkCore;
using Project_1.Data.Connection;
using Project_1.Data.Party;
using Project_1.Domain.Connection;

namespace Project_1.Infra
{
    public sealed class UniversityDb : DbContext
    {
        public UniversityDb(DbContextOptions<UniversityDb> options) : base(options) { }
        public DbSet<StudentData>? Students { get; internal set; }
        public DbSet<CourseData>? Courses { get; internal set; }
        public DbSet<LecturerData>? Lecturers { get; internal set; }
        public DbSet<StudyProgramData>? StudyPrograms { get; internal set; }
        public DbSet<SemesterData>? Semesters { get; internal set; }
        public DbSet<CountryData>? Countries { get; internal set; }
        public DbSet<CourseLecturerData>? CourseLecturers { get; internal set; }
        public DbSet<StudyProgramCourseData>? StudyProgramsCourses { get; internal set; }
        public DbSet<EnrollmentData>? Enrollments { get; internal set; }
        public DbSet<SemesterCourseData>? SemestersCourse { get; internal set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b)
        {
            var s = nameof(UniversityDb)[0..^2];
            _ = (b?.Entity<StudentData>()?.ToTable(nameof(Students), s));
            _ = (b?.Entity<LecturerData>()?.ToTable(nameof(Lecturers), s));
            _ = (b?.Entity<StudyProgramData>()?.ToTable(nameof(StudyPrograms), s));
            _ = (b?.Entity<CourseData>()?.ToTable(nameof(Courses), s));
            _ = (b?.Entity<SemesterData>()?.ToTable(nameof(Semesters), s));
            _ = (b?.Entity<CountryData>()?.ToTable(nameof(Countries), s));
            _ = (b?.Entity<CourseLecturerData>()?.ToTable(nameof(CourseLecturers), s));
            _ = b?.Entity<StudyProgramCourseData>()?.ToTable(nameof(StudyProgramsCourses), s);
            _ = b?.Entity<EnrollmentData>()?.ToTable(nameof(Enrollments), s);
            _ = b?.Entity<SemesterCourseData>()?.ToTable(nameof(SemestersCourse), s);
        }
    }
}
