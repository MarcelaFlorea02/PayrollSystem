using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Entity;
using PayrollSystem.Persistence;
using PayrollSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Services.Implementations
{
    public class PayrollService : IPayrollService
    {
        private readonly ApplicationDbContext _context;
        private decimal _contractualEarnings;
        private decimal _overtimeHours;
        public PayrollService(ApplicationDbContext context)
        {
            _context = context;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                _contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                _contractualEarnings = contractualHours * hourlyRate;
            }
            return _contractualEarnings;
        }

        public async Task CreateAsync(Payment paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Payment> GetAll() => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        //to render this in a select list in the view 
        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var allTaxYear = _context.TaxYears.Select(t => new SelectListItem
            {
                Text = t.YearOfTax,
                Value = t.Id.ToString()
            });
            return allTaxYear;
        }

        public Payment GetById(int id) => _context.PaymentRecords.FirstOrDefault(p => p.Id == id);

        public TaxYear GetTaxYearById(int id)
        {
            return _context.TaxYears.Where(t => t.Id == id).FirstOrDefault();
        }

        public decimal NetPay(decimal totalEarnings, decimal totalDeductions) => totalEarnings - totalDeductions;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeRate * overtimeHours;

        public decimal OvertimeHours(decimal hourwWorked, decimal contractualHours)
        {
            if (hourwWorked <= contractualHours)
            {
                _overtimeHours = 0.00m;
            }
            else if (hourwWorked > contractualHours)
            {
                _overtimeHours = hourwWorked - contractualHours;
            }
            return _overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic) => tax + nic;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings) => overtimeEarnings + contractualEarnings;
    }
}
