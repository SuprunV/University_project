using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_1.Pages.Extensions
{
    public static class ViewerForHtmlForm
    {
        public static IHtmlContent ViewerForShow<TModel, Tresult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, Tresult>> expression)
        {
            var s = htmlStrings(html, expression);
            return new HtmlContentBuilder(s);
        }
        public static IHtmlContent ViewerForShow<TModel, Tresult>(this IHtmlHelper<TModel> html, Expression<Func<TModel, Tresult>> expression, dynamic value) {
            var s = htmlStrings(html, expression, value);
            return new HtmlContentBuilder(s);
        }

        private static List<object> htmlStrings<TModel, Tresult>(IHtmlHelper<TModel> h, Expression<Func<TModel, Tresult>> e)
            => new List<object>() {
            new HtmlString("<dl class=\"row\">"),
                new HtmlString("<dt class=\"col-sm-2\">"),
                h.DisplayNameFor(e),
                new HtmlString(" </dt>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.DisplayFor(e),
                new HtmlString("</dd>"),
                new HtmlString("</dl>")
        };
        private static List<object> htmlStrings<TModel, Tresult>(IHtmlHelper<TModel> h, Expression<Func<TModel, Tresult>> e, dynamic value)
            => new List<object>() {
            new HtmlString("<dl class=\"row\">"),
                new HtmlString("<dt class=\"col-sm-2\">"),
                h.DisplayNameFor(e),
                new HtmlString(" </dt>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.Raw(value),
                new HtmlString("</dd>"),
                new HtmlString("</dl>")
};
    }
}
