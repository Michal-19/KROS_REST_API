using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartmentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<Department>> GetAllDepartments()
        {
            return Ok(_context.Departments);
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            var department = _context.Departments.SingleOrDefault(x => x.Id == id);
            if (department == null)
                return NotFound("Department with id " + id + " doesnt exist!");
            return Ok(department);
        }

        [HttpPost]
        public ActionResult<ICollection<Department>> AddDepartment(DepartmentDTO department)
        {
            if (department.DepartmentChiefId.HasValue)
            {
                var chief = _context.Employees.Find(department.DepartmentChiefId);
                if (chief == null)
                    return BadRequest("Wrong filled or empty employeeChiefId");
            }
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return BadRequest("Wrong filled or empty projectId");

            var newDepartment = new Department()
            {
                Name = department.Name,
                DepartmentChiefId = department.DepartmentChiefId,
                ProjectId = department.ProjectId
            };
            _context.Add(newDepartment);
            _context.SaveChanges();
            return Ok(_context.Departments);
        }

        [HttpPut]
        public ActionResult<Department> UpdateDepartment(int id, DepartmentDTO department)
        {
            var departmentToUpdate = _context.Departments.Find(id);
            if (departmentToUpdate == null)
                return NotFound("Department with id " + id + " doesnt exist");
            if (department.DepartmentChiefId.HasValue)
            {
                var chief = _context.Employees.Find(department.DepartmentChiefId);
                if (chief == null)
                    return BadRequest("Employee with id" + department.DepartmentChiefId + " doesnt exist!");
            }
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return BadRequest("Project with id" + department.ProjectId + " doesnt exist");

            departmentToUpdate.Name = department.Name;
            departmentToUpdate.DepartmentChiefId = department.DepartmentChiefId;
            departmentToUpdate.ProjectId = department.ProjectId;
            _context.SaveChanges();
            return Ok(departmentToUpdate);
        }

        [HttpDelete]
        public ActionResult<ICollection<Department>> DeleteDepartment(int id)
        {
            var departmentToDelete = _context.Departments.Find(id);
            if (departmentToDelete == null)
                return NotFound("Department with id " + id + " doesnt exist");
            _context.Remove(departmentToDelete);
            _context.SaveChanges();
            return Ok(_context.Departments);
        }
    }
}
