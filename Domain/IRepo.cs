using System.Linq.Expressions;

namespace Project_1.Domain
{ 
    public interface IRepo<T> : IPagedRepo<T> where T : UniqueEntity
    { }
    public interface IPagedRepo<T> : IOrderedRepo<T> where T : UniqueEntity {
        public int PageIndex { get; set; }
        public int TotalPages { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
        public int PageSize { get; set; }
    }
    public interface IOrderedRepo<T> : IFilteredRepo<T> where T : UniqueEntity {
        public string? CurrentOrder { get; set; }
        public string SortOrder(string propertyName);
    }
    public interface IFilteredRepo<T> : ICrudRepo<T> where T : UniqueEntity
    {
        public string? CurrentFilter { get; set; }
        public string? StudyProgramFilter { get; set; }
        public string? SemesterFilter { get; set; }
        public string? UserID { get; set; }
        public string? statusFilter { get; set; }
    }
    public interface ICrudRepo<T> : IControllerPagesRepo<T> where T : UniqueEntity { }
    public interface IControllerPagesRepo<T> : ISessionRepo<T> where T : UniqueEntity { }
    public interface ISessionRepo<T> : IBaseRepo<T> where T : UniqueEntity {
        public string SessionUserName { get; set; }
        public string SessionRoll { get; set; }
        public string SessionUserID { get; set; }
    }
    public interface IBaseRepo<T> where T : UniqueEntity
    {
        bool Add(T obj);
        List<T> Get();
        T GetBy(Func<T, bool>? condition);
        List<T> GetAll(Func<T, dynamic>? orderBy = null);
        T Get(string ID);
        bool Update(T obj);
        bool Delete(string ID);

        Task<bool> AddAsync(T obj);
        Task<List<T>> GetAsync();
        Task<T> GetAsync(string ID);
        Task<bool> UpdateAsync(T obj);
        Task<bool> DeleteAsync(string ID);
    }
}
