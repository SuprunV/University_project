using Microsoft.AspNetCore.Mvc;
using Project_1.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_1.Facade;
using System.Reflection;
using Project_1.Aids;
using Project_1.Infra.Party;

namespace Project_1.Pages
{
    public abstract class BasePage<TView, TEntity, TRepo> : SessionPage
          where TView : UniqueView, new()
          where TEntity : UniqueEntity
          where TRepo : IBaseRepo<TEntity>
    {

        protected readonly TRepo repo;
        protected abstract IActionResult redirectToEdit(TView v);
        protected abstract IActionResult redirectToDelete(string id);
        protected abstract TView toView(TEntity? entity, bool getNullVal = true);
        protected abstract TEntity toObject(TView? item);
        protected abstract IActionResult redirectToIndex(string page = "", string handler = "", bool toNativePage = true);

        [BindProperty] public TView Item { get; set; } = new TView();
        public IList<TView> Items { get; set; } = new List<TView>();
        public string ItemId => Item?.ID ?? string.Empty;
        public string? ErrorMessage { get; set; }
        public string Token => ConcurrencyToken.ToStr(Item?.Token);

        public BasePage(TRepo r) => repo = r;


        public virtual string? additionalControlString(string id) { return string.Empty; }

        protected abstract IActionResult getCreate();
        protected abstract void setAttributes(int idx, string? filter, string? order, int? itemsNr = null, string? spID = null, string? sID = null, string? Status = null, string? MandatoryCourse = null, string? nativePage = null);
        protected virtual async  Task<IActionResult> perform(Func<Task<IActionResult>> f,
            int idx, string? filter, string? order, bool removeKeys = false, int? itemsNr = null, string? spID = null, string? sID = null, string? Status = null, string? UserID = null, string? nativePage = null) {
            setAttributes(idx, filter, order, itemsNr, spID, sID, Status, UserID, nativePage);
            if (removeKeys)
                removeKey(nameof(filter), nameof(order));
            return await f();
        }

        protected abstract Task<IActionResult> postCreateAsync();
        protected abstract Task<IActionResult> getDetailsAsync(string id);
        protected abstract Task<IActionResult> getDeleteAsync(string id);
        protected abstract Task<IActionResult> postDeleteAsync(string id);
        protected abstract Task<IActionResult> getEditAsync(string id);
        protected abstract Task<IActionResult> postEditAsync();
        protected abstract Task<IActionResult> getIndexAsync();

        internal virtual void removeKey(params string[] keys) {
            foreach (var key in keys ?? Array.Empty<string>())
                Safe.Run(() => ModelState.Remove(key));
        }

        public virtual IActionResult OnGetCreate(int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null) {
            setAttributes(pageIndex, currentFilter, sortOrder, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage);
            return getCreate();
        }

        public virtual string getMandatoryCourse(string? IsMandatory)
        {
            if (IsMandatory == "on" && SessionRoll == "student") {
                return SessionUserID ?? String.Empty;
            }
            return string.Empty;
        }

        public virtual async Task<IActionResult> OnPostCreateAsync(int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
            => await perform(() => postCreateAsync(), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnGetDetailsAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => getDetailsAsync(id), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnGetDeleteAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => getDeleteAsync(id), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnPostDeleteAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => postDeleteAsync(id), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnGetEditAsync(string id, int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => getEditAsync(id), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnPostEditAsync(int pageIndex = 0, int? itemsNr = null, string? currentFilter = null, string? sortOrder = null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => postEditAsync(), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
        public virtual async Task<IActionResult> OnGetIndexAsync(int pageIndex=0, int? itemsNr = null, string? currentFilter = null, string? sortOrder =null, string? StudyProgramID = null, string? SemesterID = null, string? Status = null, string? IsMandatory = null, string? nativePage = null)
             => await perform(() => getIndexAsync(), pageIndex, currentFilter, sortOrder, true, itemsNr, StudyProgramID, SemesterID, Status, getMandatoryCourse(IsMandatory), nativePage );
    }
}