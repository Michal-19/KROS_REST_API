using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ICollection<Department>> GetAllDepartments()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var department = _service.GetOne(id);
            if (department == null)
                return NotFound("Department with id " + id + " doesnt exist!");
            return Ok(department);  
        }

        [HttpPost]
        public ActionResult<ICollection<Department>> AddDepartment(CreateDepartmentDTO department)
        {
            var addedDepartment = _service.Add(department);
            if (addedDepartment == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(addedDepartment);
        }

        [HttpPut]
        public ActionResult<Department> UpdateDepartment(int id, UpdateDepartmentDTO department)
        {
            var updatedDepartment = _service.Update(id, department);
            if (updatedDepartment == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(updatedDepartment);
        }

        [HttpDelete]
        public ActionResult<ICollection<Department>> DeleteDepartment(int id)
        {
            var deletedDepartment = _service.Delete(id);
            if (deletedDepartment == null)
                return NotFound("Department with id " + id + " doesnt exist!");
            return Ok(deletedDepartment);
        }
    }
}
