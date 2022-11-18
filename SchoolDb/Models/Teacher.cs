using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDb.Models
{
    public class Teacher
    {
        //the following fields define a teacher
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public decimal Salary;
        public DateTime HireDate;
    }
}