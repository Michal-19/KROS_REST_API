using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] 
        public ActionResult<List<Employee>> GetAllEmployees() 
        {
            return Ok(_context.Employees.Include(x => x.Companies));
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id) 
        {
            var employee = _context.Employees.Include(x=> x.Companies).Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<List<Employee>> AddEmployee(EmployeeDTO employee) 
        {
            Employee newEmployee = new Employee()
            {
                Degree = employee.Degree,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                TelephoneNumber = employee.TelephoneNumber
            };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return Ok(_context.Employees.Include(x => x.Companies));
        }

        [HttpPut]
        public ActionResult<Employee> UpdateEmployee(int id, EmployeeDTO updatedEmployee) 
        {
            var employee = _context.Employees.Include(x => x.Companies).Where(x => x.Id == id).FirstOrDefault();
            if (employee == null) 
                return NotFound("Employee with id " + id + " doesnt exist!");
            employee.Degree = updatedEmployee.Degree;
            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Email = updatedEmployee.Email;
            employee.TelephoneNumber = updatedEmployee.TelephoneNumber;
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        public ActionResult<List<Employee>> DeleteEmployee(int id)
        {
            var employeeToDelete = _context.Employees.Include(x => x.Companies).Where(x => x.Id == id).FirstOrDefault();
            if (employeeToDelete == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            _context.Remove(employeeToDelete);
            _context.SaveChanges();
            return Ok(_context.Employees);
        }
    }
}
