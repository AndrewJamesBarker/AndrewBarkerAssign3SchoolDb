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
                Int32 TeacherId = (Int32)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
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

        /// <summary>
        /// returns details on teacher. id, first and last name, salary and hire date
        /// </summary>
        /// <param name="id"></param>
        /// <returns>http://localhost:50226/Teacher/Show/2  Name: Caitlin Cummings
        ///Employee Number: T381
        ///Salary: $62.77
        ///Hire Date: 10-Jun-2014</returns>
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
                Int32 TeacherId = (Int32)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherLname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                decimal Salary = (decimal)ResultSet["salary"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;
            }


            return NewTeacher;
        }
        /// <summary>
        /// deletes a teacher from the database
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST : http://localhost:50226/api/TeacherData/DeleteTeacher/3 </example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //create an instance of a connections
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and the database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "Delete from teachers where teacherid =@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }
        /// <summary>
        /// add new teacher to database. id, first and last name, salary and hiredate
        /// </summary>
        /// <param name="NewTeacher"></param>
        /// <returns>takes you back to teacher list</returns>
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //create an instance of a connections
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and the database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, salary, hiredate) values (@TeacherFname,@TeacherLname,@employeenumber,@Salary,@HireDate)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        /// <summary>
        /// supposed to allow you to update teacher info, but can't get past id error 
        /// The parameters dictionary contains a null entry for parameter 'id' of non-nullable type 'System.Int32'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherInfo"></param>
        /// POST: /api/teacherdata/updateteacher/4
        /// <returns>updated info teacher first and last name, id, salary and hiredate.</returns>
        /// requests BODY POST DATa
        [HttpPost]
        [Route("api/teacherdata/updateteacher/{id}")]
        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //create an instance of a connections
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and the database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname," +
                " employeenumber=@employeenumber, salary=@Salary, hiredate=@HireDate where teacherid =@id";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@id", id);
            //cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

    }
}
