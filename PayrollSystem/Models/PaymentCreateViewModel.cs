using PayrollSystem.Entity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace PayrollSystem.Models
{
    public class PaymentCreateViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public string NationalInsuranceNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; } = "1250L";
        public decimal HourlyRate { get; set; }

        public decimal HoursWorked { get; set; }
        public decimal ContractualHours { get; set; } = 144m;
        public decimal OvertimeHours { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal OvertimeEarnings { get; set; }

        public decimal Tax { get; set; }

        public decimal NIC { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetPayment { get; set; }
    }
}
