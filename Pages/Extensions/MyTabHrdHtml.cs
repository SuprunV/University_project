using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_1.Facade.Connection;
using Project_1.Facade.Party;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Pages.Extensions
{
    public static class MyTabHrdHtml
    {
        public static IHtmlContent MyTabHdr<TModel>(
            this IHtmlHelper<TModel> h, string name, string handler)
        {
            var s = htmlStrings(name, handler, h.ViewData.Model as IPageModel);
            return new HtmlContentBuilder(s);
        }
        private static List<object> htmlStrings(string name, string handler, IPageModel? m)
        {
            var l = new List<object>();
            if (correctSortOrderItem(handler)) l.Add(new HtmlString($"<a style=\"text-decoration:none;\" href=\"{ m?.GenerateUrl(m, null, handler) }\">{name}</a>"));
            else l.Add(new HtmlString($"{ name }"));
            return l;
        }
        public static bool correctSortOrderItem(string item) {
            return ! new List<string>() {
                nameof(CourseView.StudyProgramID),
                nameof(CourseView.SemesterID),
                nameof(EnrollmentView.Status)
            }.Contains(item);
        
        } 
    private static string? pageName(IPageModel? m) => m?.GetType()?.Name?.Replace("Pages", "");
    }
}
