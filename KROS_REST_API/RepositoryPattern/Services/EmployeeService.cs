using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
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
        public ICollection<Employee> GetAll()
        {
            return _context.Employees
                                     .Include(x => x.DivisionsChief)
                                     .Include(x => x.ProjectsChief)
                                     .Include(x => x.DepartmentsChief).ToList();
        }

        public Employee? GetOne(int id)
        {
            var employee = _context.Employees
                                             .Include(x => x.DivisionsChief)
                                             .Include(x => x.ProjectsChief)
                                             .Include(x => x.DepartmentsChief)
                                             .SingleOrDefault(x => x.Id == id);
            if (employee == null)
                return null;
            return employee;
        }

        public ICollection<Employee>? Add(CreateEmployeeDTO employee)
        {
            var company = _context.Companies.Find(employee.CompanyId);
            if (company == null)
                return null;
            var newEmployee = new Employee()
            {
                Degree = employee.Degree,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                TelephoneNumber = employee.TelephoneNumber,
                CompanyWorkId = employee.CompanyId
            };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return _context.Employees
                                     .Include(x => x.DivisionsChief)
                                     .Include(x => x.ProjectsChief)
                                     .Include(x => x.DepartmentsChief).ToList();
        }

        public Employee? Update(int id, UpdateEmployeeDTO employee)
        {
            var employeeToUpdate = _context.Employees
                                                     .Include(x => x.DivisionsChief)
                                                     .Include(x => x.ProjectsChief)
                                                     .Include(x => x.DepartmentsChief)
                                                     .SingleOrDefault(x => x.Id == id);
            if (employeeToUpdate == null)
                return null;
            employeeToUpdate.Degree = employee.Degree;
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Email = employee.Email;
            employeeToUpdate.TelephoneNumber = employee.TelephoneNumber;
            _context.SaveChanges();
            return employeeToUpdate;
        }

        public ICollection<Employee>? Delete(int id)
        {
            var employeeToDelete = _context.Employees.SingleOrDefault(x => x.Id == id);
            if (employeeToDelete == null)
                return null;
            _context.Remove(employeeToDelete);
            _context.SaveChanges();
            return _context.Employees.Include(x => x.DivisionsChief)
                                     .Include(x => x.ProjectsChief)
                                     .Include(x => x.DepartmentsChief).ToList();
        }
    }
}
