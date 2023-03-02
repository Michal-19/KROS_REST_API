using KROS_REST_API.Data;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

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
            var employees = _context.Employees.ToList();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id) 
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employee == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<List<Employee>> AddEmployee(Employee employee) 
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok(_context.Employees);
        }

        [HttpPut]
        public ActionResult<Employee> UpdateEmployee(int id, Employee updatedEmployee) 
        {
            var employee = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
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
            var employeeToDelete = _context.Employees.Where(x => x.Id == id).FirstOrDefault();
            if (employeeToDelete == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            _context.Remove(employeeToDelete);
            _context.SaveChanges();
            return Ok(_context.Employees);
        }
    }
}
