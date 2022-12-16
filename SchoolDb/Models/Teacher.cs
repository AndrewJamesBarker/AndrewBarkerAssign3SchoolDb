using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace SchoolDb.Models
{

    //server side diagnostics in the model...
    public class Teacher
    {
        //the following fields define a teacher
        public Int32 TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public decimal Salary;
        public DateTime HireDate;

        public bool IsValid()
        {
            bool valid = true;

            if (TeacherFname == null || TeacherLname == null || EmployeeNumber == null || Salary == null || HireDate == null)
            {
                valid = false;
            }

            else
            {
                if (TeacherFname.Length < 2 || TeacherFname.Length > 255) valid = false;
                if (TeacherLname.Length < 2 || TeacherLname.Length > 255) valid = false;
                if (EmployeeNumber.Length < 1) valid = false;
               
            }
            return valid;
        }
    }
}