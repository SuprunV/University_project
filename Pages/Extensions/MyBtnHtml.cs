using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Pages.Extensions {
    public static class MyBtnHtml {
        public static IHtmlContent MyBtn<TModel>(
            this IHtmlHelper<TModel> h, string handler, string id, bool isGetOnly = false, string joinedTxt = "") {
            var s = htmlStrings(handler, id, isGetOnly, joinedTxt, h.ViewData.Model as IPageModel);
            return new HtmlContentBuilder(s);
        }
        private static List<object> htmlStrings(string handler, string id, bool isGetOnly, string joinedTxt, IPageModel? m) {
            var l = new List<object>();
            var rootPage = getRootFolder(m);
            var pageName = m?.GetType()?.Name?.Replace("Pages", "");
            var linkPage = "/" + ((handler == "Details" || handler == "Edit" || handler == "Delete") ? handler :
                           (pageName == "Courses" || pageName == "Enrollments") ? "Index" :
                           pageName + "Index");
            var linkBase = $"{rootPage}{linkPage}";
            l.Add(new HtmlString($"<a href=\"/{linkBase}?"));
            l.Add(new HtmlString($"handler={handler}&amp;"));
            l.Add(new HtmlString($"nativePage={ m?.getNativePageName() ?? "Index" }&amp;"));
            l.Add(new HtmlString($"id={id}&amp;"));
            l.Add(new HtmlString($"sortOrder={m?.CurrentOrder}&amp;"));
            l.Add(new HtmlString($"pageIndex={m?.PageIndex ?? 0}&amp;"));
            l.Add(new HtmlString($"currentFilter={m?.CurrentFilter}&amp;"));
            l.Add(new HtmlString($"itemsNr={m?.PageSize}\">"));
            l.Add(new HtmlString($"{(handler == "Join" ? joinedTxt : handler)}</a>"));
            return l;
        }

        private static string? getHandler(IPageModel? m, string handler)
        {
            var pageName = m?.GetType()?.Name?.Replace("Pages", "");
            //if (pageName == "JoinedCourses" || pageName == "MyCourses") return "Courses";
            if (pageName == "Enrollments" && handler == "Details" ) return "EnrollmentDetails";
            return handler;
        }
        private static string? getRootFolder(IPageModel? m) {
            var pageName = m?.GetType()?.Name?.Replace("Pages", "");
            if (pageName == "JoinedCourses" || pageName == "MyCourses") return "Courses";
            else if  (pageName == "ChooseCourses") return "Enrollments";
            return pageName;
        }
    }
}
