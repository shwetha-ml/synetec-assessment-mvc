using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Exceptions
{
    public class SalaryInvalidException : Exception
    {
        public SalaryInvalidException()
        {
        }

        public SalaryInvalidException(int id)
            : base($"Employee with Id {id} has an invalid salary.")
        {
        }
    }
}