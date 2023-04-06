using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Entity;
using PayrollSystem.Models;
using PayrollSystem.Services.Interfaces;
using System;





using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment hostingEnvironment)
        {
            _employeeService = employeeService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int? pageNumber)
        {
            var employees = _employeeService.GetAll().Select(e => new EmployeeIndexViewModel()
            {
                Id = e.Id,
                EmployeeNo = e.EmployeeNo,
                FullName = e.FullName,
                Gender = e.Gender,
                Role = e.Role,
                City = e.City,
                DateJoined = e.DateJoined
            }).ToList();
             int pageSize = 1;
           // return View(employees);
              return View(EmployeeListPagination<EmployeeIndexViewModel>.Create(employees, pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]// Prevents cross-site Request Forgery Attacks 
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    DateJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    PaymentMethod = model.PaymentMethod,
                    Address = model.Address,
                    City = model.City,
                    Role = model.Role,
                    MiddleName = model.MiddleName,
                    PostCode = model.PostCode
                };
         
                await _employeeService.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();

            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                Address = employee.Address,
                City = employee.City,
                Role = employee.Role,
                MiddleName = employee.MiddleName,
                PostCode = employee.PostCode
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetById(model.Id);
                if (employee == null)
                    return NotFound();

                employee.EmployeeNo = model.EmployeeNo;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.Gender = model.Gender;
                employee.Email = model.Email;
                employee.DateOfBirth = model.DateOfBirth;
                employee.DateJoined = model.DateJoined;
                employee.NationalInsuranceNo = model.NationalInsuranceNo;
                employee.PaymentMethod = model.PaymentMethod;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Role = model.Role;
                employee.MiddleName = model.MiddleName;
                employee.PostCode = model.PostCode;

                await _employeeService.UpdateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();
            var model = new EmployeeDetailViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                Gender = employee.Gender,
                Email = employee.Email,
                DateOfBirth = employee.DateOfBirth,
                DateJoined = employee.DateJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.PaymentMethod,
                Address = employee.Address,
                City = employee.City,
                Role = employee.Role,
                PostCode = employee.PostCode
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
                return NotFound();
            var model = new EmployeeDeleteViewModel() { Id = id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeService.DeleteAsync(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
