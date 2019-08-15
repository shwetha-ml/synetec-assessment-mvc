using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.ViewModels
{
    public class GetDetailsForSelectedEmployeeViewModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bonus pool amount needs to be greater than 0.")]
        public int? BonusPool { get; set; }
        public List<HrEmployeeConciseViewModel> Employees { get; set; }
        public int SelectedEmployeeId { get; set; }

        public GetDetailsForSelectedEmployeeViewModel()
        {
            this.Employees = new List<HrEmployeeConciseViewModel>();
        }
    }

    public class HrEmployeeConciseViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}