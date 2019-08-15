using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Exceptions
{
    public class TotalDepartmentSalaryInvalidException : Exception
    {
        public TotalDepartmentSalaryInvalidException()
        {
        }

        public TotalDepartmentSalaryInvalidException(int id)
            : base($"Total salary for personnel in department with Id {id} is invalid.")
        {
        }
    }
}