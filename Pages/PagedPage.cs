using System.ComponentModel;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Aids;
using Project_1.Domain;
using Project_1.Facade;

namespace Project_1.Pages
{
    public abstract class PagedPage<TView, TEntity, TRepo> : OrderedPage<TView, TEntity, TRepo>,
        IPageModel, IIndexModel<TView>
        where TView : UniqueView, new()
        where TEntity : UniqueEntity
        where TRepo : IPagedRepo<TEntity> {
        protected PagedPage(TRepo r) : base(r) { }

        public int PageIndex {
            get => repo.PageIndex;
            set => repo.PageIndex = value;
        }

        public int PageSize {
            get => repo.PageSize;
            set => repo.PageSize = value;
        }
        public string? NativePage { get; set; }
        public int TotalPages => repo.TotalPages;
        public bool HasNextPage => repo.HasNextPage;
        public bool HasPreviousPage => repo.HasPreviousPage;


        public string getNativePageName() {
            var mainPart = $"{this?.GetType()?.Name?.Replace("Pages", string.Empty) ?? ""}";
            if (mainPart is "Students" or "Lecturers" or "Semesters" or "StudyPrograms" or "Courses" or "Enrollments") return "Index";
            else return mainPart + "Index";
        }

        protected override void setAttributes(int idx, string? filter, string? order, int? itemsNr = null, string? spID = null, string? sID = null, string? Status = null, string? userID = null, string? nativePage = null) {
            PageIndex = idx;
            CurrentFilter = filter;
            CurrentOrder = order;
            PageSize = itemsNr ?? 10;
            StudyProgramFilter = spID;
            SemesterFilter = sID;
            statusFilter = Status;
            UserID = userID;
            NativePage = nativePage ?? String.Empty;
            repo.SessionUserID = SessionUserID ?? String.Empty ;
            repo.SessionRoll = SessionRoll ?? String.Empty;
            repo.SessionUserName = SessionUserName ?? String.Empty;
        }
        protected override IActionResult redirectToIndex(string page = "", string handler = "", bool toNativePage = true) => Redirect(GenerateUrl(this, null, null, toNativePage));

        protected override IActionResult redirectToEdit(TView v)
        {
            TempData["Item"] = JsonSerializer.Serialize(v);
            return RedirectToPage("./Edit", "Edit",
                new
                {
                    id = v.ID,
                    pageIndex = PageIndex,
                    currentFilter = CurrentFilter,
                    sortOrder = CurrentOrder
                });
        }
        protected override IActionResult redirectToDelete(string id)
        {
            TempData["Error"] = "The record you attempted to delete "
                                + "was modified by another user after you selected delete. "
                                + "The delete operation was canceled and the current values in the "
                                + "database have been displayed. If you still want to delete this "
                                + "record, click the Delete button again.";

            return RedirectToPage("./Delete", "Delete",
                new
                {
                    id = id,
                    pageIndex = PageIndex,
                    currentFilter = CurrentFilter,
                    sortOrder = CurrentOrder
                });
        }
        public virtual string[] IndexColumns => Array.Empty<string>();
        public virtual Dictionary<string, IEnumerable<SelectListItem>> SelectFilters => new ();
        public virtual List<string> CheckBoxFilters => new ();
        public virtual object? GetValue(string name, TView v)
            => Safe.Run(() => {
                var pi = v?.GetType()?.GetProperty(name);
                return pi == null ? null : showValue(pi.GetValue(v));
            }, null);
        public string? DisplayName(string name) => Safe.Run(() => {
            var p = typeof(TView).GetProperty(name);
            var a = p?.CustomAttributes?
                .FirstOrDefault(x => x.AttributeType == typeof(DisplayNameAttribute));
            return a?.ConstructorArguments[0].Value?.ToString() ?? name;
        }, name);
        public object? showValue(object? v) {
            return (v as DateTime?)?.ToString("dd/MM/yyyy") ?? v;
        }
        public string GetSelectValue(string key) {
            switch (key) {
                case "StudyProgramID": return StudyProgramFilter ?? String.Empty;
                case "SemesterID": return SemesterFilter ?? String.Empty;
                case "Status": return Convert.ToString(statusFilter) ?? String.Empty;
            }
            return key;
        }
        public string GenerateUrl<TModel>(TModel m, Actions? action = null, string? sortOrderElem = null, bool useNativePage = false) where TModel : IPageModel {
            var pageName = m?.GetType()?.Name?.Replace("Pages", string.Empty) ?? "";
            var pageLink = "";
            var pageHandler = "";
            foreach (var item in m?.AccessRights ?? new Dictionary<string, Dictionary<string, List<string>>>()) {
                if (item.Key == pageName)
                {
                    pageLink = useNativePage ? m?.NativePage : item.Value["path"][0];
                    string[] pathParts = ((item.Value["path"][0]).Split("/"));
                    //var lastElem = pathParts[pathParts.Count() - 1];
                    pageHandler = "Index";
                    break;
                };

            }

            if (string.IsNullOrEmpty(pageLink)) pageLink = "Index";
            if (string.IsNullOrEmpty(pageHandler)) pageHandler = "Index";

            int? pageIndexVal = 0;

            switch(action) {
                case Actions.ClearPage:
                    return getModelUrl(m, pageLink, pageHandler, 0, "", "", "", "", "", "", 10);
                case Actions.First:
                case null:
                    pageIndexVal = PageIndex;
                    break;
                case Actions.Last:
                    pageIndexVal = m?.TotalPages - 1;
                    break;
                default:
                    pageIndexVal = m?.PageIndex + (int)action;
                    break;
            }

            sortOrderElem = sortOrderElem == null ? sortOrderElem : SortOrder(sortOrderElem);

            var url = getModelUrl(m, pageLink, pageHandler, pageIndexVal, sortOrderElem);
            
            return url;
        }
        public string getModelUrl<TModel>(TModel? m, string? pageLink, string? pageHandler, int? pageIndex,
                                          string? so = null, string? cf = null, string? sp = null, string? se = null,
                                          string? st = null, string? im = null, int? it = null, string? np = null)  where TModel : IPageModel => 
                $"{ pageLink }?"
                + $"handler={ pageHandler }&"
                + $"pageIndex={ pageIndex }&" // prev: -1, next: +1, first: 0, last: PageSize
                + $"sortOrder={ so ?? m?.CurrentOrder }&"
                + $"currentFilter={ cf ?? m?.CurrentFilter }&"
                + $"StudyProgramID={ sp ?? m?.StudyProgramFilter }&" // how to add StudyProgram
                + $"SemesterID={ se ?? m?.SemesterFilter }&" // how to add SemesterID
                + $"Status={ st ?? m?.statusFilter }&" //  how to add Status
                + $"NativeNamePage={ np ?? m?.NativePage }&"
                + $"IsMandatory={im ?? m?.IsMandatory}&"//  how to add MandatoryCourse
                + $"itemsNr={ it ?? m?.PageSize }";
    }


    public enum Actions {
        Last = -3,
        Prev = -1,
        First = 0,
        Next = 1,
        ClearPage = -2
    }
}