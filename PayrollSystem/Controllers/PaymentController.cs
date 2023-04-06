using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Entity;
using PayrollSystem.Models;
using PayrollSystem.Services.Interfaces;
using RotativaCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class PaymentController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaxService _taxService;
        private readonly INIService _NIService;
        private decimal ovH;
        private decimal ovE;
        private decimal cE;
        private decimal tE;
        private decimal sl;
        private decimal uf;
        private decimal t;
        private decimal nic;
        private decimal tD;

        public PaymentController(IPayrollService payrollService, IEmployeeService employeeService, ITaxService taxService, INIService nIService)
        {
            _payrollService = payrollService;
            _employeeService = employeeService;
            _taxService = taxService;
            _NIService = nIService;
        }

        public IActionResult Index()
        {
            var payments = _payrollService.GetAll().Select(p => new PaymentIndexViewModel()
            {
                Id = p.Id,
                Employee = p.Employee,
                FullName = p.FullName,
                NetPayment = p.NetPayment,
                PayDate = p.PayDate,
                PayMonth = p.PayMonth,
                TaxYearId = p.TaxYearId,
                TotalDeduction = p.TotalDeduction,
                TotalEarnings = p.TotalEarnings,
                Year = _payrollService.GetTaxYearById(p.TaxYearId).YearOfTax
            });
            return View(payments);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payrollService.GetAllTaxYear();
            var model = new PaymentCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PaymentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var paymentRecord = new Payment()
                {
                    Id = model.Id,
                    ContractualEarnings = cE = _payrollService.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    ContractualHours = model.ContractualHours,
                    EmployeeId = model.EmployeeId,
                    FullName = _employeeService.GetById(model.EmployeeId).FullName,
                    HourlyRate = model.HourlyRate,
                    HoursWorked = model.HoursWorked,
                    NationalInsuranceNo = _employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    NIC = nic = _NIService.GetNIContribution(tE),
                    OvertimeHours = ovH = _payrollService.OvertimeHours(model.HoursWorked, model.ContractualEarnings),
                    OvertimeEarnings = ovE = _payrollService.OvertimeEarnings(_payrollService.OvertimeRate(model.HourlyRate), ovH),
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxCode = model.TaxCode,
                    TaxYear = model.TaxYear,
                    TaxYearId = model.TaxYearId,
                    TotalEarnings = tE = _payrollService.TotalEarnings(ovE, cE),
                    Tax = t = _taxService.GetTaxAmount(tE),
                    TotalDeduction = tD = _payrollService.TotalDeduction(t, nic),
                    NetPayment = _payrollService.NetPay(tE, tD),
                };
                await _payrollService.CreateAsync(paymentRecord);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employees = _employeeService.GetAllEmployeesForPayroll();
            ViewBag.taxYears = _payrollService.GetAllTaxYear();
            return View();
        }

        public IActionResult Detail(int id)
        {
            var payment = _payrollService.GetById(id);
            if (payment == null)
                return NotFound();

            var model = new PaymentDetailViewModel()
            {
                ContractualEarnings = payment.ContractualEarnings,
                ContractualHours = payment.ContractualHours,
                EmployeeId = payment.EmployeeId,
                FullName = payment.FullName,
                HourlyRate = payment.HourlyRate,
                HoursWorked = payment.HoursWorked,
                Id = payment.Id,
                NationalInsuranceNo = payment.NationalInsuranceNo,
                NetPayment = payment.NetPayment,
                NIC = payment.NIC,
                OvertimeEarnings = payment.OvertimeEarnings,
                OvertimeHours = payment.OvertimeHours,
                PayDate = payment.PayDate,
                PayMonth = payment.PayMonth,
                Tax = payment.Tax,
                TaxCode = payment.TaxCode,
                TaxYear = payment.TaxYear,
                TaxYearId = payment.TaxYearId,
                TotalDeduction = payment.TotalDeduction,
                TotalEarnings = payment.TotalEarnings,
                OvertimeRate = _payrollService.OvertimeRate(payment.HourlyRate),
                Employee = payment.Employee,
                Year = _payrollService.GetTaxYearById(payment.TaxYearId).YearOfTax,
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Payslip(int id)
        {
            var payment = _payrollService.GetById(id);
            if (payment == null)
                return NotFound();

            var model = new PaymentDetailViewModel()
            {
                ContractualEarnings = payment.ContractualEarnings,
                ContractualHours = payment.ContractualHours,
                EmployeeId = payment.EmployeeId,
                FullName = payment.FullName,
                HourlyRate = payment.HourlyRate,
                HoursWorked = payment.HoursWorked,
                Id = payment.Id,
                NationalInsuranceNo = payment.NationalInsuranceNo,
                NetPayment = payment.NetPayment,
                NIC = payment.NIC,
                OvertimeEarnings = payment.OvertimeEarnings,
                OvertimeHours = payment.OvertimeHours,
                PayDate = payment.PayDate,
                PayMonth = payment.PayMonth,
                Tax = payment.Tax,
                TaxCode = payment.TaxCode,
                TaxYear = payment.TaxYear,
                TaxYearId = payment.TaxYearId,
                TotalDeduction = payment.TotalDeduction,
                TotalEarnings = payment.TotalEarnings,
                Year = _payrollService.GetTaxYearById(payment.TaxYearId).YearOfTax,
                OvertimeRate = _payrollService.OvertimeRate(payment.HourlyRate),
                Employee = payment.Employee
            };
            return View(model);
        }

        public IActionResult GeneratePayslipPdf(int id)
        {
            var payslip = new ActionAsPdf("Payslip", new { id = id })
            {
                FileName = "payslip.pdf"
            };
            return payslip;
        }
    }
}
