using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDb.Models;

namespace SchoolDb.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course/List
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string SearchKey = null)
        {
            CoursesDataController controller = new CoursesDataController();
            IEnumerable<Course> Courses = controller.ListCourses(SearchKey);
            return View(Courses);
        }

        // GET : Course/Show/{id}
        public ActionResult Show(int id)
        {

            CoursesDataController controller = new CoursesDataController();
            Course NewCourse = controller.FindCourse(id);
            


            return View(NewCourse);

        }


    }
}