using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Facade;

namespace Project_1.Pages.Extensions {
    public static class MyLinkHtml
    {
        public static IHtmlContent ShowLink<TModel, TView>(this IHtmlHelper<TModel> h, TModel m, string linkText = "")
        where TModel : IIndexModel<TView> where TView : UniqueView
        {
            var s = htmlStrings(linkText, h.ViewData.Model as IPageModel);
            return new HtmlContentBuilder(s);
        }
        public static List<object> htmlStrings(string linkText, IPageModel? m) {

            var pageName = m?.GetType()?.Name?.Replace("Pages", string.Empty) ?? "";

            var pageLink = "";
            var pageHandler = "";
            foreach (var item in m?.AccessRights ?? new Dictionary<string, Dictionary<string, List<string>>>())
            {
                if (item.Key == pageName) {
                    pageLink = item.Value["path"][0];
                    string[] pathParts = ((item.Value["path"][0]).Split("/"));
                    var lastElem = pathParts[pathParts.Count() - 1];
                    pageHandler = lastElem;
                    break;
                };

            }
            
            if (string.IsNullOrEmpty(pageLink)) pageLink = "Index";
            if (string.IsNullOrEmpty(pageHandler)) pageHandler = "Index";

            var l = new List<object>();
            l.Add(new HtmlString($"<a href={ pageLink }"));
            l.Add(new HtmlString($"handler={ pageHandler }&amp;"));
            l.Add(new HtmlString($"sortOrder={m?.CurrentOrder}&amp;"));
            l.Add(new HtmlString($"pageIndex={m?.PageIndex ?? 0}&amp;"));
            l.Add(new HtmlString($"currentFilter={m?.CurrentFilter}&amp;"));
            l.Add(new HtmlString($"itemsNr={ linkText }\">"));
            l.Add(new HtmlString($"</a>"));
            return l;
        }
    }
}
