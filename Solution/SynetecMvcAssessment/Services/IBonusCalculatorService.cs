using InterviewTestTemplatev2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTestTemplatev2.Services
{
    public interface IBonusCalculatorService
    {
        int CalculateBonus(HrEmployee employee, int bonusPool);
        int CalculateBonusBasedOnDepartmentAllocation(HrEmployee employee, int bonusPool);
    }
}
