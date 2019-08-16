using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Exceptions
{
    public class SalaryInvalidException : Exception
    {
        public SalaryInvalidException()
            : base("Employee has an invalid salary.")
        {
        }
    }
}