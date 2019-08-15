using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Services
{
    public class BonusCalculatorService : IBonusCalculatorService
    {
        private IRepository<HrEmployee> _employeeRepository;
        private IRepository<HrDepartment> _departmentRepository;

        public BonusCalculatorService(IRepository<HrEmployee> employeeRepository, IRepository<HrDepartment> departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public int CalculateBonus(HrEmployee employee, int bonusPool)
        {
            //get the total salary budget for the company
            int totalSalary = _employeeRepository.GetAll().Sum(item => item.Salary);

            //calculate the bonus allocation for the employee ie (employeeSalary / totalSalary) * bonusPool
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            var bonusAmount = (int)(bonusPercentage * bonusPool);

            return bonusAmount;
        }

        public int CalculateBonusBasedOnDepartmentAllocation(HrEmployee employee, int bonusPool)
        {
            // calculate bonus allocation for department
            var bonusAllocationPercForDept = _departmentRepository.Get(employee.HrDepartmentId).BonusPoolAllocationPerc;

            if (!bonusAllocationPercForDept.HasValue)
                throw new Exception($"Department with Id {employee.HrDepartmentId} does not have allocation percentage and hence bonus based on department allocation cannot be computed");

            var bonusAllocationForDept = bonusAllocationPercForDept.Value * bonusPool;

            int totalDepartmentSalary = _employeeRepository.GetAll().Where(emp => emp.HrDepartmentId == employee.HrDepartmentId).Sum(item => item.Salary);

            //calculate the bonus allocation for the employee ie (employeeSalary / totalDepartmentSalary) * bonusPool
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalDepartmentSalary;

            var bonusAmount = (int)(bonusPercentage * bonusPool);

            return bonusAmount;
        }
    }
}