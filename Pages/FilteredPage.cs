using Project_1.Domain;
using Project_1.Facade;

namespace Project_1.Pages
{
    public abstract class FilteredPage<TView, TEntity, TRepo> : CrudPage<TView, TEntity, TRepo>
        where TView : UniqueView, new()
        where TEntity : UniqueEntity
        where TRepo : IFilteredRepo<TEntity>
    {
        protected FilteredPage(TRepo r) : base(r) { }
        public string? CurrentFilter {
            get => repo.CurrentFilter;
            set => repo.CurrentFilter = value;
        }
        public string? SemesterFilter
        {
            get => repo.SemesterFilter;
            set => repo.SemesterFilter = value;
        }
        public string? StudyProgramFilter
        {
            get => repo.StudyProgramFilter;
            set => repo.StudyProgramFilter = value;
        }
        public string? UserID
        {
            get => repo.UserID;
            set => repo.UserID= value;
        }
        public string? IsMandatory { get; set; }
        public string? statusFilter
        {
            get => repo.statusFilter;
            set => repo.statusFilter = value;
        }
    }
}