using Microsoft.AspNetCore.Mvc.Rendering;
using PayrollSystem.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PayrollSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);
        Employee GetById(int id);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int id);
        Task DeleteAsync(int id);
        IEnumerable<Employee> GetAll();
        IEnumerable<SelectListItem> GetAllEmployeesForPayroll();
    }
}
