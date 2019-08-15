using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Data.Repositories;
using InterviewTestTemplatev2.Services;
using InterviewTestTemplatev2.ViewModels;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private IBonusCalculatorService _bonusCalculatorService;
        private IRepository<HrEmployee> _employeeRepository;

        public BonusPoolController(IBonusCalculatorService bonusCalculatorService, IRepository<HrEmployee> employeeRepository)
        {
            _bonusCalculatorService = bonusCalculatorService;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Gets the bonus pool amount and the employee for whom the bonus needs to be calculated
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailsForSelectedEmployee()
        {
            GetDetailsForSelectedEmployeeViewModel model = new GetDetailsForSelectedEmployeeViewModel();
            model.Employees = _employeeRepository.GetAll().Select(Mapper.Map<HrEmployee, HrEmployeeConciseViewModel>).ToList();
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateForSelectedEmployee(GetDetailsForSelectedEmployeeViewModel model)
        {
            HrEmployee thisEmployee = _employeeRepository.Get(model.SelectedEmployeeId);

            if (thisEmployee == null)
                throw new Exception("Invalid employee");
            
            BonusForEmployeeViewModel result = new BonusForEmployeeViewModel();
            result.EmployeeFullName = thisEmployee.Full_Name;
            result.BonusAmount = _bonusCalculatorService.CalculateBonus(thisEmployee, model.BonusPool.Value);
            
            return View(result);
        }


        /// <summary>
        /// Gets the bonus pool amount
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetailsForAllEmployees()
        {
            GetDetailsForAllEmployeesViewModel model = new GetDetailsForAllEmployeesViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateForAllEmployees(GetDetailsForAllEmployeesViewModel model)
        {
            var employeeBonusDetails = new List<BonusForEmployeeViewModel>();

            foreach (var employee in _employeeRepository.GetAll())
            {
                var bonusAmount = _bonusCalculatorService.CalculateBonus(employee, model.BonusPool.Value);
                employeeBonusDetails.Add(new BonusForEmployeeViewModel { BonusAmount = bonusAmount, EmployeeFullName = employee.Full_Name });
            }

            return View(employeeBonusDetails);
        }
    }
}