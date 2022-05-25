using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Pages {
    public interface IPageModel {
        public int PageIndex { get; }
        public int PageSize { get; }
        public string? CurrentFilter { get; }
        public string? SemesterFilter { get ; }
        public string? NativePage { get; }
        public string? StudyProgramFilter { get; }
        public string? IsMandatory { get; }
        public string? statusFilter { get; }
        public int TotalPages { get; }
        public string? CurrentOrder { get; }
        public Dictionary<string, Dictionary<string, List<string>>> AccessRights { get; }
        public string? SortOrder(string propertyName);
        public string GenerateUrl<TModel>(TModel m, Actions? action = null, string? sortOrderElem = null, bool useNativePage = false) where TModel : IPageModel;
        public string getNativePageName();
    }
}
