using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Facade;

namespace Project_1.Pages.Extensions;

public static class ShowTableHtml {
    public static  IHtmlContent ShowTable<TModel, TView>(this IHtmlHelper<TModel>? h, IList<TView>? items)
        where TModel : IIndexModel<TView> where TView : UniqueView {
        var s = htmlStrings(h, items);
        return new HtmlContentBuilder(s);
    }
    public static List<object?> htmlStrings<TModel, TView>(IHtmlHelper<TModel>? h, IList<TView>? items)
        where TModel : IIndexModel<TView> where TView : UniqueView {
        var m = h!.ViewData.Model;
        var pageName = m?.GetType()?.Name?.Replace("Pages", "")?? "";
        var allowedActions = h?.ViewData?.Model?.AccessRights?[pageName]["actions"] ?? null;
        var l = new List<object?>();
        if (items?.Count > 0) {
            l.Add(new HtmlString("<table class=\"table\">"));
            l.Add(new HtmlString("<thead>"));
            l.Add(new HtmlString("<tr>"));
            foreach (var name in m!.IndexColumns) {
                l.Add(new HtmlString("<th>"));
                l.Add(h?.MyTabHdr(m.DisplayName(name) ?? string.Empty, name));
                l.Add(new HtmlString("</th>"));
            }
            l.Add(new HtmlString("<th></th>"));
            l.Add(new HtmlString("</tr>"));
            l.Add(new HtmlString("</thead>"));
            l.Add(new HtmlString("<tbody>"));
            foreach (var item in items ?? new List<TView>())
            {
                l.Add(new HtmlString("<tr>"));
                foreach (var name in m.IndexColumns)
                {
                    l.Add(new HtmlString("<td>"));
                    l.Add(h?.Raw(m.GetValue(name, item)));
                    l.Add(new HtmlString("</td>"));
                }
                l.Add(new HtmlString("<td>"));
                var joinedTxt = m.additionalControlString(item.ID);
                l.Add(h?.ItemButtons(item.ID, allowedActions, joinedTxt!));
                l.Add(new HtmlString("</td>"));
                l.Add(new HtmlString("</tr>"));
            }
            l.Add(new HtmlString("</tbody>"));
            l.Add(new HtmlString("</table>"));
        }
        else {
            l.Add(new HtmlString("<div class\"text-center\"><h4><br/>The information hasn't been added yet! Please wait and then refresh the page!</h4><br/></div>"));
        }
        return l;
    }
}