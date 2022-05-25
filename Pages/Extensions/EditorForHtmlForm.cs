using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace Project_1.Pages.Extensions
{
    public static class EditorForHtmlForm
    {
        public static IHtmlContent EditorForInput<TModel, TResult> (
            this IHtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression) {
            var s = htmlStringsForm(html, expression);
            return new HtmlContentBuilder(s);
        }

        private static List<object> htmlStringsForm<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e)
        {
            var l = new List<object>();
            l.Add(new HtmlString("<dl class=\"row\">"));
            l.Add(new HtmlString("<dd class=\"col-sm-2\">"));
            l.Add(h.LabelFor(e, null, new { @class = "control-label" }));
            l.Add(new HtmlString("</dd>"));
            l.Add(new HtmlString("<dd class=\"col-sm-10\">"));
            l.Add(h.EditorFor(e, new { htmlAttributes = new { @class = "form-control" } }));
            l.Add(h.ValidationMessageFor(e,null, new { @class = "text-danger" }));
            l.Add(new HtmlString("</dd>"));
            l.Add(new HtmlString("</dl>"));
            return l;
        }
    }
}
