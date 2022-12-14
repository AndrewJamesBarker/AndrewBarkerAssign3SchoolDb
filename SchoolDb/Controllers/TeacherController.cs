using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDb.Models;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace SchoolDb.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.DataTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);

        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }


        /// <summary>
        /// generates new teacher data
        /// </summary>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="EmployeeNumber"></param>
        /// <param name="Salary"></param>
        /// <param name="HireDate"></param>
        /// <returns> creates sends new teacher data to be viewed. teacher first and last name, id, salary and hiredate</returns>
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary, DateTime HireDate)
        {
            //identify if it is running
            //identify if the inputs provided by the form 
            Debug.WriteLine("I have accesses the create method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);
            Debug.WriteLine(HireDate);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;
            NewTeacher.HireDate = HireDate;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher); 

            return RedirectToAction("List");
        }
        /// <summary>
        /// routes to a dynamically generated "teacher page" and gathers info from the database
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <returns>a dynamic update author page displaying the current teacher info and asking for new info</returns>
        //Get : /Teacher/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        /// <summary>
        /// Receives a Post request with info on an existing teacher, with new values. 
        /// Conveys this info to the api, and redirects to the "Teacher Show" page of the updated teacher.
        /// </summary>
        /// <param name="id">not sure what to do here</param>
        /// <param name="TeacherFname">updated first name</param>
        /// <param name="TeacherLname">updated last name</param>
        /// <param name="EmployeeNumber">updated employee number</param>
        /// <param name="Salary">updated salary</param>
        /// <param name="HireDate">updated hire date</param>
        /// <returns>returns a dynamic webpage with the updated teacher info</returns>
        /// <example>POST : /Teacher/Update/{id}
        /// {
        /// "TeacherFname":"Joe",
        /// "TeacherLname": "Shmoe",
        /// "EmployeeNumber": "T654",
        /// "Salary": "56.6",
        /// "HireDate": "2022-05-09"
        /// }
        /// </example>
        //Post : /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary, DateTime HireDate)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.Salary = Salary;
            TeacherInfo.HireDate = HireDate;
           

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }


}