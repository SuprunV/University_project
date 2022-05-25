using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_1.Pages.Extensions {
    public static class ItemButtonsHtml {
        public static IHtmlContent ItemButtons<TModel>(
            this IHtmlHelper<TModel> h, string id, List<string>? accessedActions, string joinedTxt = "") {
            var s = htmlStrings(h, id, joinedTxt, accessedActions);
            return new HtmlContentBuilder(s);
        }
        private static List<object> htmlStrings<TModel>(IHtmlHelper<TModel> h, string id, string joinedTxt, List<string>? accessedActions) {
            var l = new List<object>();
            accessedActions?.Remove("Create");
            foreach (var action in accessedActions ?? new List<string>()) {
                l.Add(h.MyBtn(action, id, action == "Join", joinedTxt));
                l.Add(new HtmlString("&nbsp;"));
            }
            return l;
        }
    }
}
