using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.ViewModels
{
    public class GetDetailsForAllEmployeesViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bonus pool amount needs to be greater than 0.")]
        public int? BonusPool { get; set; }
    }
}