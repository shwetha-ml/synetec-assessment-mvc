using System;
using InterviewTestTemplatev2.Data;
using InterviewTestTemplatev2.Data.Repositories;
using InterviewTestTemplatev2.Exceptions;
using InterviewTestTemplatev2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SynetecMvcAssessment.Test
{
    [TestClass]
    public class BonusCalculatorTest
    {
        private IRepository<HrEmployee> _fakeEmployeeRepository;
        private IRepository<HrDepartment> _fakeDepartmentRepository; 

        [TestMethod]
        public void CalculateBonus_WithAllValuesSupplied_ReturnsBonusAmount()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = calculator.CalculateBonus(100, 1000, 500);

            Assert.AreEqual(result, 200);
        }

        [TestMethod]
        public void CalculateBonus_WithSalaryValueAsZero_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<SalaryInvalidException>(() => calculator.CalculateBonus(0, 1000, 500));

            Assert.AreEqual(result.Message, "Employee has an invalid salary.");
        }

        [TestMethod]
        public void CalculateBonus_WithBudgetValueAsZero_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<TotalSalaryInvalidException>(() => calculator.CalculateBonus(100, 1000, 0));

            Assert.AreEqual(result.Message, "Total salary amount of company is invalid.");
        }

        [TestMethod]
        public void CalculateBonusBasedOnDepartmentAllocation_WithAllValuesSupplied_ReturnsBonusAmount()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = calculator.CalculateBonusBasedOnDepartmentAllocation(100, 1000,10000, 10 );

            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void CalculateBonusBasedOnDepartmentAllocation_WithSalaryValueAsZero_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<SalaryInvalidException>(() => calculator.CalculateBonusBasedOnDepartmentAllocation(0, 1000, 10000, 10));

            Assert.AreEqual(result.Message, "Employee has an invalid salary.");
        }

        [TestMethod]
        public void CalculateBonusBasedOnDepartmentAllocation_WithBonusAllocationAsNull_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<BonusAllocationNotSpecifiedForDepartmentException>(() => calculator.CalculateBonusBasedOnDepartmentAllocation(100, 1000, 10000, null));

            Assert.AreEqual(result.Message, "Bonus allocation percentage not specified for department.");
        }

        [TestMethod]
        public void CalculateBonusBasedOnDepartmentAllocation_WithBonusAllocationAsZero_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<BonusAllocationNotSpecifiedForDepartmentException>(() => calculator.CalculateBonusBasedOnDepartmentAllocation(100, 1000, 10000, 0));

            Assert.AreEqual(result.Message, "Bonus allocation percentage not specified for department.");
        }

        [TestMethod]
        public void CalculateBonusBasedOnDepartmentAllocation_WithTotalDepartmentSalaryAsZero_ThrowsException()
        {
            BonusCalculatorService calculator = new BonusCalculatorService(_fakeEmployeeRepository, _fakeDepartmentRepository);

            var result = Assert.ThrowsException<TotalDepartmentSalaryInvalidException>(() => calculator.CalculateBonusBasedOnDepartmentAllocation(100, 1000, 0, 10));

            Assert.AreEqual(result.Message, "Total salary for personnel in department is invalid.");
        }
    }
}
