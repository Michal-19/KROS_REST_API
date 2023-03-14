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
        public ActionResult<ICollection<Department>> AddDepartment(CreateDepartmentDTO department)
        {
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return BadRequest("Wrong filled or empty projectId");
            var division = _context.Divisions.Find(project.DivisionId);
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = _context.Employees.Find(department.DepartmentChiefId);
                if (departmentChief == null)
                    return BadRequest("Wrong filled or empty employeeChiefId");
                if (division.CompanyId != departmentChief.CompanyWorkId)
                    return BadRequest("Employee with id " + departmentChief.Id + " work in other company!");
            }
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
        public ActionResult<Department> UpdateDepartment(int id, UpdateDepartmentDTO department)
        {
            var departmentToUpdate = _context.Departments.Find(id);
            if (departmentToUpdate == null)
                return NotFound("Department with id " + id + " doesnt exist");
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return BadRequest("Wrong filled or empty projectId!");
            var division = _context.Divisions.Find(project.DivisionId);
            var projectWithUpdatedDepartent = _context.Projects.Find(departmentToUpdate.ProjectId);
            var divisionWithUpdatedDepartment = _context.Divisions.Find(projectWithUpdatedDepartent.DivisionId);
            if (division.CompanyId != divisionWithUpdatedDepartment.CompanyId)
                return BadRequest("Project with id " + department.ProjectId + " is from other company!");
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = _context.Employees.Find(department.DepartmentChiefId);
                if (departmentChief == null)
                    return BadRequest("Employee with id" + department.DepartmentChiefId + " doesnt exist!");
                if (departmentChief.CompanyWorkId != divisionWithUpdatedDepartment.CompanyId)
                    return BadRequest("Employee with id " + departmentChief.Id + " is from other company");
            }
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
