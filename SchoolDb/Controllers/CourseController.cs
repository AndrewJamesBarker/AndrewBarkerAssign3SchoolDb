using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDb.Models;
using System.Diagnostics;

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

        // GET : /Course/DeleteConfirm/{id} 
        public ActionResult DeleteConfirm(int id)
        {
            CoursesDataController controller = new CoursesDataController();
            Course NewCourse = controller.FindCourse(id);
            return View(NewCourse);
        }

        //POST : /Course/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CoursesDataController controller = new CoursesDataController();
            controller.DeleteCourse(id);
            return RedirectToAction("List");
        }

        //GET : /Course/New/
        public ActionResult New()
        {
            return View();
        }

        //POST : /Course/Create
        public ActionResult Create(string ClassCode,  DateTime StartDate, DateTime FinishDate, string ClassName)
        {
            //make sur its running
            //identify the inputs provided from the form


            Debug.WriteLine("I Have accessed the create method!");
            Debug.WriteLine(ClassCode);
            //Debug.WriteLine(TeacherId);
            Debug.WriteLine(StartDate);
            Debug.WriteLine(FinishDate);
            Debug.WriteLine(ClassName);

            Course NewCourse = new Course();
            NewCourse.ClassCode = ClassCode;
            //NewCourse.TeacherId = TeacherId;
            NewCourse.StartDate = StartDate;
            NewCourse.FinishDate = FinishDate;
            NewCourse.ClassName = ClassName;

            CoursesDataController controller = new CoursesDataController();
            controller.AddCourse(NewCourse);



            return RedirectToAction("List");
        }


    }
        
}