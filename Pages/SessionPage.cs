using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Project_1.Pages {
    public abstract class SessionPage : PageModel {

        public string SessionUserName {
            get => initializeSessionVar("_UserName", "");
            set { setSession("_UserName", value); }
        }
        public string? SessionRoll {
            get => initializeSessionVar("_Roll", ""); 
            set { setSession("_Roll", value); }
        }
        public string? SessionUserID {
            get => initializeSessionVar("_UserID", ""); 
            set { setSession("_UserID", value); }
        }

        public void setSession(string sessionName, string? value) {
            if (!string.IsNullOrEmpty(sessionName) && !string.IsNullOrEmpty(value))
                HttpContext.Session.SetString(sessionName, value);
        }
        public void removeSession(string sessionName) {
            if (!string.IsNullOrEmpty(sessionName))
                HttpContext.Session.Remove(sessionName);
        }

        public string initializeSessionVar(string filed, string defaultVal)
            => (string.IsNullOrEmpty(HttpContext.Session.GetString(filed))) ? defaultVal : HttpContext.Session.GetString(filed);
    }
}
