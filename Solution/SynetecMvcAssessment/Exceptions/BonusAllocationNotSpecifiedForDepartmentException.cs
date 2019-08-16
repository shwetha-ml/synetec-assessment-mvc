using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Exceptions
{
    public class BonusAllocationNotSpecifiedForDepartmentException : Exception
    {
        public BonusAllocationNotSpecifiedForDepartmentException()
            : base("Bonus allocation percentage not specified for department.")
        {
        }
    }
}