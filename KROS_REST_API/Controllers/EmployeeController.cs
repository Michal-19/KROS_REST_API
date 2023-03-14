using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet] 
        public ActionResult<ICollection<Employee>> GetAllEmployees() 
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(int id) 
        {
            var employee = _service.GetOne(id);
            if (employee== null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<ICollection<Employee>> AddEmployee(CreateEmployeeDTO employee) 
        {
            var addedEmployee = _service.Add(employee);
            if (addedEmployee == null) 
                return BadRequest();
            return Ok(addedEmployee);
        }

        [HttpPut]
        public ActionResult<Employee> Update(int id, UpdateEmployeeDTO employee) 
        {
            var updatedEmployee = _service.Update(id, employee);
            if (updatedEmployee == null)
                return BadRequest();
            return Ok(updatedEmployee);
        }

        [HttpDelete]
        public ActionResult<ICollection<Employee>> Delete(int id)
        {
            var deletedEmployee = _service.Delete(id);
            if (deletedEmployee == null)
                return NotFound();
            return Ok(deletedEmployee);
        }
    }
}
