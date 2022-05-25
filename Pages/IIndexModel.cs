using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Facade;

namespace Project_1.Pages {
    public interface IIndexModel<TView> where TView : UniqueView {
        public string[] IndexColumns { get; }
        public Dictionary<string, IEnumerable<SelectListItem>> SelectFilters { get; }
        public Dictionary<string, Dictionary<string, List<string>>> AccessRights { get; }
        public IList<TView>? Items { get; }
        public object? GetValue(string name, TView v);
        string? DisplayName(string name);
        public string? additionalControlString(string id);
    }
}
