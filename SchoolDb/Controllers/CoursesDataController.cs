using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolDb.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace SchoolDb.Controllers
{
    public class CoursesDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// returns list of courses in the system
        /// </summary>
        /// <example>GET api/CoursesData/ListCourses</example>
        /// <returns>a list of courses</returns>
        [HttpGet]
        [Route("api/CourseData/ListCourses/{SearchKey?}")]
        public IEnumerable<Course> ListCourses(string SearchKey = null)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between server and database
            Conn.Open();


            string query = "Select * from classes where lower(classname) like lower(@key) or classid like (@key)";
            Debug.WriteLine("the search key is " + query);

            //establish a new command queery for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Quesry
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result set of query into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty list of Courses
            List<Course> Courses = new List<Course> { };

            while (ResultSet.Read())
            {
                //access column information by the db column name as an index
                int ClassId = (int)ResultSet["classid"];
                string ClassCode = (string)ResultSet["classcode"];
                int TeacherId = ResultSet.GetInt32("teacherid");
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = (string)ResultSet["classname"];

                Course NewCourse = new Course();
                NewCourse.ClassId = ClassId;
                NewCourse.ClassCode = ClassCode;
                NewCourse.TeacherId = TeacherId;
                NewCourse.StartDate = StartDate;
                NewCourse.FinishDate = FinishDate;
                NewCourse.ClassName = ClassName;

                //add the course info to the list
                Courses.Add(NewCourse);
            }
            Conn.Close();
            //return the final list of courses
            return Courses;
        }
        /// <summary>
        /// returns a single instance of a course
        /// </summary>
        /// <param name="id">class id</param>
        /// <returns>info on a particular course based on classid input</returns>
        [HttpGet]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between server and database
            Conn.Open();

            //establish a new command queery for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from classes where classid = " + id;

            //Gather Result set of query into variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //access column information by the db column name as an index
                int ClassId = (int)ResultSet["classid"];
                int TeacherId = (int)ResultSet["teacherid"];
                string ClassCode = (string)ResultSet["classcode"];
            
                DateTime StartDate = (DateTime)ResultSet["startdate"];
                DateTime FinishDate = (DateTime)ResultSet["finishdate"];
                string ClassName = (string)ResultSet["classname"];

                NewCourse.ClassId = ClassId;
                NewCourse.ClassCode = ClassCode;
                NewCourse.TeacherId = TeacherId;
                NewCourse.StartDate = StartDate;
                NewCourse.FinishDate = FinishDate;
                NewCourse.ClassName = ClassName;
            }

            return NewCourse;
        }
        /// <summary>
        /// deletes a course by course id
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST: /api/CoursesData/DeleteCourse/3</example>
        [HttpPost]
        public void DeleteCourse(int id)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between server and database
            Conn.Open();

            //establish a new command queery for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from classes where classid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// adds new course object with all criteria except teacher id foreign key
        /// </summary>
        /// <param name="NewCourse"></param>
        /// <example>POST: curl -H "Content-Type: application/json" -d "{\"ClassCode\":\"Test Class\",\"StartDate\":
        /// \"june 3 2023\",\"FinishDate\":\"june 4 2023\",\"ClassName\":\"how to test asp.net\"}" </example>
        [HttpPost]
        public void AddCourse([FromBody]Course NewCourse)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between server and database
            Conn.Open();

            //establish a new command queery for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "insert into classes (classcode, teacherid, startdate, finishdate, classname) values (@ClassCode, @TeacherId," +
                " @StartDate,@FinishDate,@ClassName)";
            cmd.Parameters.AddWithValue("@ClassCode", NewCourse.ClassCode);
            cmd.Parameters.AddWithValue("@TeacherId", NewCourse.TeacherId);
            cmd.Parameters.AddWithValue("@StartDate", NewCourse.StartDate);
            cmd.Parameters.AddWithValue("@FinishDate",NewCourse.FinishDate );
            cmd.Parameters.AddWithValue("@ClassName", NewCourse.ClassName);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
      


    }
}
