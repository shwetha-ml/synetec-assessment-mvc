using InterviewTestTemplatev2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewTestTemplatev2.Services
{
    public class BonusCalculatorService : IBonusCalculatorService
    {
        private MvcInterviewV3Entities1 _context;

        public BonusCalculatorService()
        {
            _context = new MvcInterviewV3Entities1();
        }

        public int CalculateBonus(HrEmployee employee, int bonusPool)
        {
            //get the total salary budget for the company
            int totalSalary = _context.HrEmployees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee ie (employeeSalary / totalSalary) * bonusPool
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            var bonusAmount = (int)(bonusPercentage * bonusPool);

            return bonusAmount;
        }

        public int CalculateBonusBasedOnDepartmentAllocation(HrEmployee employee, int bonusPool)
        {
            // calculate bonus allocation for department
            var bonusAllocationPercForDept = _context.HrDepartments.Single(dept => dept.ID == employee.HrDepartmentId).BonusPoolAllocationPerc;

            if (!bonusAllocationPercForDept.HasValue)
                throw new Exception($"Department with Id {employee.HrDepartmentId} does not have allocation percentage and hence bonus based on department allocation cannot be computed");

            var bonusAllocationForDept = bonusAllocationPercForDept.Value * bonusPool;

            int totalDepartmentSalary = _context.HrEmployees.Where(emp => emp.HrDepartmentId == employee.HrDepartmentId).Sum(item => item.Salary);

            //calculate the bonus allocation for the employee ie (employeeSalary / totalDepartmentSalary) * bonusPool
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalDepartmentSalary;

            var bonusAmount = (int)(bonusPercentage * bonusPool);

            return bonusAmount;
        }
    }
}