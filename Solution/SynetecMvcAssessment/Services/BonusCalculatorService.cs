using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterviewTestTemplatev2.Exceptions;

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

        /// <summary>
        /// Calculate the employee's bonus based on proportion of the employee's salary on the total wage budget of the company
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="bonusPool"></param>
        /// <returns></returns>
        public int CalculateBonus(HrEmployee employee, int bonusPool)
        {          
            //get the total salary budget for the company
            int totalSalary = GetTotalSalaryOfAllCompanyPersonnel();
            
            return CalculateBonus(employee.Salary, bonusPool, totalSalary);
        }

        /// <summary>
        /// Calculate the employee's bonus based on proportion of the employee's salary on the total wage budget of the company
        /// </summary>
        /// <param name="employeeSalary"></param>
        /// <param name="bonusPool"></param>
        /// <param name="totalSalaryBudget"></param>
        /// <returns></returns>
        public int CalculateBonus(int employeeSalary, int bonusPool, int totalSalaryBudget)
        {
            if (employeeSalary <= 0)
                throw new SalaryInvalidException();

            if (totalSalaryBudget <= 0)
                throw new TotalSalaryInvalidException();

            // Calculate the bonus allocation for the employee ie (employeeSalary / totalSalary) * bonusPool
            decimal bonusPercentage = (decimal)employeeSalary / (decimal)totalSalaryBudget;
            var bonusAmount = (int)(bonusPercentage * bonusPool);
            return bonusAmount;
        }

        /// <summary>
        /// Get the sum of all salaries paid to all personnel in the company 
        /// </summary>
        /// <returns></returns>
        private int GetTotalSalaryOfAllCompanyPersonnel()
        {
            return _employeeRepository.GetAll().Sum(item => item.Salary);
        }

        /// <summary>
        /// Calculate the employee's bonus based on the employee's department allocation percentage
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="bonusPool"></param>
        /// <returns></returns>
        public int CalculateBonusBasedOnDepartmentAllocation(HrEmployee employee, int bonusPool)
        {
            // calculate bonus allocation for department
            var bonusAllocationPercForDept = _departmentRepository.Get(employee.HrDepartmentId).BonusPoolAllocationPerc;            

            // Compute the sum of all salaries paid to personnel in this department
            int totalDepartmentSalary = GetTotalSalaryOfAllPersonnelInDepartment(employee.HrDepartmentId);

            return CalculateBonusBasedOnDepartmentAllocation(employee.Salary, bonusPool, employee.HrDepartmentId, bonusAllocationPercForDept);
        }

        /// <summary>
        /// Get the sum of all salaries paid to personnel in this department 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        private int GetTotalSalaryOfAllPersonnelInDepartment(int departmentId)
        {
            return _employeeRepository.GetAll().Where(emp => emp.HrDepartmentId == departmentId).Sum(item => item.Salary);
        }

        /// <summary>
        /// Calculate the employee's bonus based on the employee's department allocation percentage
        /// </summary>
        /// <param name="employeeSalary"></param>
        /// <param name="bonusPool"></param>
        /// <param name="employeeDepartmentId"></param>
        /// <param name="bonusAllocationPercentageForDept"></param>
        /// <returns></returns>
        public int CalculateBonusBasedOnDepartmentAllocation(int employeeSalary, int bonusPool, int totalDepartmentSalary, int? bonusAllocationPercentageForDept)
        {
            if (!bonusAllocationPercentageForDept.HasValue || bonusAllocationPercentageForDept.Value == 0)
                throw new BonusAllocationNotSpecifiedForDepartmentException();

            if (employeeSalary <= 0)
                throw new SalaryInvalidException();

            if (totalDepartmentSalary <= 0)
                throw new TotalDepartmentSalaryInvalidException();
            
            // Retrieve the bonus allocation % for this department
            var bonusPoolAllocationForDept = (bonusAllocationPercentageForDept.Value * bonusPool) / 100;            

            // Calculate the bonus allocation for the employee ie (employeeSalary / totalDepartmentSalary) * bonusPoolAllocationForDept
            decimal bonusPercentageForEmployee = (decimal)employeeSalary / (decimal)totalDepartmentSalary;
            var bonusAmount = (int)(bonusPercentageForEmployee * bonusPoolAllocationForDept);
            return bonusAmount;
        }
    }
}