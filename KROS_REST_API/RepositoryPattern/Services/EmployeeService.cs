using KROS_REST_API.Data;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetOne(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;
            return employee;
        }

        public async Task<ICollection<Employee>?> Add(Employee employee)
        {
            var company = await _context.Companies.FindAsync(employee.CompanyWorkId);
            if (company == null)
                return null;
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> Update(int id, Employee employee)
        {
            var employeeToUpdate = await _context.Employees.FindAsync(id);
            if (employeeToUpdate == null)
                return null;
            employeeToUpdate.Degree = employee.Degree;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Email = employee.Email;
            employeeToUpdate.TelephoneNumber = employee.TelephoneNumber;
            await _context.SaveChangesAsync();
            return employeeToUpdate;
        }

        public async Task<ICollection<Employee>?> Delete(int id)
        {
            var employeeToDelete = await _context.Employees.FindAsync(id);
            if (employeeToDelete == null)
                return null;
            _context.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
            return await _context.Employees.ToListAsync();
        }
    }
}
