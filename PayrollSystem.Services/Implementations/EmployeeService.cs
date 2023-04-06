using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Entity;
using PayrollSystem.Persistence;
using PayrollSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = GetById(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
            // return _context.Employees.AsNoTracking().OrderBy(emp => emp.FullName);
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayroll()
        {
            var employees = GetAll();
            return employees.Select(e => new SelectListItem()
            {
                Text = e.FullName,
                Value = e.Id.ToString()
            });
        }

        public Employee GetById(int id)
        {
            return _context.Employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}

