using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Exceptions
{
    public class TotalSalaryInvalidException : Exception
    {
        public TotalSalaryInvalidException()
            : base("Total salary amount of company is invalid.")
        {
        }
    }
}