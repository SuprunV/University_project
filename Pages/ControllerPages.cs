using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_1.Domain;
using Project_1.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1.Pages {
    //Control, what pages can be showed and what user can do on this pages
    public abstract class ControllerPages<TView, TEntity, TRepo> : BasePage<TView, TEntity, TRepo>
        where TView : UniqueView, new()
        where TEntity : UniqueEntity
        where TRepo : IControllerPagesRepo<TEntity>
    {
        protected ControllerPages(TRepo r) : base(r) { }
        public Dictionary<string, Dictionary<string, List<string>>> AccessRights { get { return getHeaderLinks(); } set { } }
        public Dictionary<string, Dictionary<string, List<string>>> getHeaderLinks() {
            //Allows Pages contains pages name (full path to file by Project_1.Pages) as key and 
            //List of available actions (that can do on this page) as value
            var list = new Dictionary<string, Dictionary<string, List<string>>>();
            switch (SessionRoll) {
                case "admin":
                    list = new() {
                        { "Lecturers", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() { "Lecturers" } },
                            { "showInHeader", new List<string>()  { "True" } },
                            { "path", new List<string>()  { "/Lecturers/Index" } },
                            { "actions", new List<string>()  { "Edit", "Delete", "Details", "Create" } }
                        } },
                        {
                            "Students", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() { "Students" } },
                            { "showInHeader", new List<string>()  { "True" } },
                            { "path", new List<string>()  { "/Students/Index" } },
                            { "actions", new List<string>()  { "Edit", "Delete", "Details", "Create" } }
                        } },
                        {
                            "StudyPrograms", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() { "Study programs" } },
                            { "showInHeader", new List<string>() { "True" } },
                            { "path", new List<string>()  { "/StudyPrograms/Index" } },
                            { "actions", new List<string>() { "Edit", "Delete", "Details", "Create" } }
                        } },
                        {
                            "Semesters", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() { "Semesters" } },
                            { "showInHeader", new List<string>() { "True" } },
                            { "path", new List<string>()  { "/Semesters/Index" } },
                            { "actions", new List<string>() { "Edit", "Delete", "Details", "Create" } }
                        } }
                    };
                break;
                case "lecturer":
                    list = new() {
                        { "Courses", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() {"Available courses"} },
                            { "showInHeader", new List<string>()  { "True"} },
                            { "path", new List<string>()  { "/Courses/Index"} },
                            { "actions", new List<string>()  { "Details", "Join" } }
                        } },
                        { "MyCourses", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() {"My courses"} },
                            { "showInHeader", new List<string>()  { "True" } },
                            { "path", new List<string>()  { "/Courses/MyCoursesIndex" } },
                            { "actions", new List<string>()  { "Edit", "Delete", "Details", "Create" } }
                        } },
                        {
                            "JoinedCourses", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() {"Joined courses"} },
                            { "showInHeader", new List<string>()  { "False"} },
                            { "path", new List<string>()  { "/Courses/JoinedCoursesIndex" } },
                            { "actions", new List<string>()  { "Details", "Join" } }
                        } },
                        {
                            "Enrollments", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() {"Assign enrollments"} },
                            { "path", new List<string>()  { "/Enrollments/Index" } },
                            { "showInHeader", new List<string>()  { "True"} },
                            { "actions", new List<string>()  { "Details", "Edit" } }
                        } }
                    };
                    break;
                case "student":
                    list = new() {
                        {
                            "ChooseCourses", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() {"Available courses"} },
                            { "path", new List<string>()  { "/Enrollments/ChooseCoursesIndex"} },
                            { "showInHeader", new List<string>()  { "True" } },
                            { "actions", new List<string>()  { "Details" } }
                        } },
                        {
                            "Enrollments", new Dictionary<string, List<string>>() {
                            { "name", new List<string>() { "Evaluation paper" } },
                            { "path", new List<string>()  { "/Enrollments/Index" } },
                            { "showInHeader", new List<string>()  { "True"} },
                            { "actions", new List<string>()  { "Details", "Join" } }
                        } },
                    };
                    break;
            }
            return list;
        }
    }
}
