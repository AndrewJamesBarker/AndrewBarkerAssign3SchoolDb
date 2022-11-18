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
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        // this controller will access the teachers table of our database.
        /// <summary>
        /// returns data on the teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/DataTeachers</example>
        /// <param name="Searchkey">the text t search against</param>
        /// <returns>data on the teachers, fname lname income, and hiredate</returns>
        [HttpGet]
        [Route("api/TeacherData/DataTeacher/{SearchKey?}")]
        public IEnumerable<Teacher> DataTeachers(string SearchKey = null)
        {
            //create an instance of a connections
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and the database
            Conn.Open();

            //establish a new command (query) for our database
         

            //sql query
            string query = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key)" +
                " or lower(concat(teacherfname, ' ', teacherlname) like lower(@key))"
                ;

            Debug.WriteLine("the search key is " +query);

            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@key", "%"+SearchKey+"%");
            cmd.Prepare();

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty list of teachers
            //List<Teacher> TeacherInfo = new List<Teacher> { };
            List<Teacher> TeacherInfo = new List<Teacher> ();

            //loop through each row of the result set

            while (ResultSet.Read())
            {
                //access column info by the db column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;
                
                // add the info to the list
                TeacherInfo.Add(NewTeacher);

            }
            //close the connection between the MySQL database and the server
            Conn.Close();

            //Return the final list of teacher names and salaries
            return TeacherInfo;
        }
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //create an instance of a connections
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and the database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            //gather result set of query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //access column info by the db column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;
            }


            return NewTeacher;
        }


    }
}
