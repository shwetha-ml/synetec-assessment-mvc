# Synetec Basic ASP.Net MVC assessement - Shwetha's notes

## Bonus calculation

The problem statement in the 'MVC Developer Test Instructions' PDF states that the proportion of the bonus pool allocated to an employee should be the same as the proportion of the total wage budget that their salary represents. 

In the database schema provided, the 'HrDepartment' table contains a column 'BonusPoolAllocationPerc'. This is a percentage of the bonus pool that is allocated to the department. I was unsure if this needs to be factored in the bonus calculation algorithm. 

So, in order to provide a complete solution, I have included CalculateBonusBasedOnDepartmentAllocation() in the BonusCalculatorService that uses the 'BonusPoolAllocationPerc' value to compute the bonus for the employee. Please note that the view and controller invoke the CalculateBonus() method which computes the bonus simply as a proportion of the employee's salary to the total salary budget.

## Bonus calculation for one employee vs all employees

The problem statement in the 'MVC Developer Test Instructions' PDF states that the task in hand is to build a prototype web app to calculate the individual bonus amounts for *each* employee. Since the views in the solution is calculating the bonus for the selected employee, I have also built the functionality to compute and display the bonus allocations for all employees given the bonus pool amount. This view can be accessed via ../BonusPool/GetDetailsForAllEmployees url.

## Validations

Certain validations have been implemented -

1. Bonus pool amount is required
2. Bonus pool amount should be greater than 0

## Exceptions

Custom exceptions have been created - 

1. BonusAllocationNotSpecifiedForDepartmentException - Thrown by the BonusCalculatorService.CalculateBonusBasedOnDepartmentAllocation() if the bonus allocation percentage is not specified for department in question
2. EmployeeNotFoundException - Thrown by the BonusPoolController if employee with does not exist. It is unlikely that this exception will ever be thrown but implemented to provide completeness 
3. SalaryInvalidException - Thrown by the BonusCalculatorService.CalculateBonus() and BonusCalculatorService.CalculateBonusBasedOnDepartmentAllocation() if salary of employee is less than or equal to zero
4. TotalDepartmentSalaryInvalidException - Thrown by the BonusCalculatorService.CalculateBonusBasedOnDepartmentAllocation() if total salary for personnel in department is less than or equal to zero
5. TotalSalaryInvalidException -  Thrown by the BonusCalculatorService.CalculateBonus() if total salary amount of company is less than or equal to zero
