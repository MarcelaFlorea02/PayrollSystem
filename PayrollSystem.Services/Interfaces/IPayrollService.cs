using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollSystem.Services.Interfaces
{
    public interface IPayrollService
    {
        Task CreateAsync(Payment paymentRecord);
        Payment GetById(int id);
        TaxYear GetTaxYearById(int id);
        IEnumerable<Payment> GetAll();
        IEnumerable<SelectListItem> GetAllTaxYear();
        decimal OvertimeHours(decimal hourwWorked, decimal contractualHours);
        decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);
        decimal OvertimeRate(decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);
        decimal TotalDeduction(decimal tax, decimal nic);
        decimal NetPay(decimal totalEarnings, decimal totalDeductions);
    }
}
