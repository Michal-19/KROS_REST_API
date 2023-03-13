using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<Project>> GetAllProjects()
        {
            return Ok(_context.Projects.Include(x => x.Departments));
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProjectById(int id)
        {
            var project = _context.Projects.Include(x => x.Departments).SingleOrDefault(x => x.Id == id);
            if (project == null)
                return NotFound("Project with id " + id + " doesnt exist!");
            return Ok(project);
        }

        [HttpPost]
        public ActionResult<ICollection<Project>> AddProject(CreateProjectDTO project)
        {
            if (project.ProjectChiefId.HasValue)
            {
                var chief = _context.Employees.SingleOrDefault(x => x.Id == project.ProjectChiefId);
                if (chief == null)
                    return BadRequest("Employee with id " + project.ProjectChiefId + " doesnt exist!");
            }
            var division = _context.Divisions.SingleOrDefault(x => x.Id == project.DivisionId);
            if (division == null)
                return BadRequest("Wrong filled or empty DivisionId field");
            var newProject = new Project()
            {
                Name = project.Name,
                ProjectChiefId = project.ProjectChiefId,
                DivisionId = project.DivisionId
            };
            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return Ok(_context.Projects);
        }

        [HttpPut]
        public ActionResult<Project> UpdateProject(int id, UpdateProjectDTO project)
        {
            var projectToUpdate = _context.Projects.Include(x => x.Departments).SingleOrDefault(x => x.Id == id);
            if (projectToUpdate == null)
                return NotFound("Project with id + " + id + " doesnt exist!");
            if (project.ProjectChiefId.HasValue)
            {
                var projectChief = _context.Employees.Find(project.ProjectChiefId);
                if (projectChief == null)
                    return BadRequest("Empoyee with id" + project.ProjectChiefId + " doesnt exist!");
            }
            projectToUpdate.Name = project.Name;
            projectToUpdate.ProjectChiefId = project.ProjectChiefId;
            _context.SaveChanges();
            return Ok(projectToUpdate);
        }

        [HttpDelete]
        public ActionResult<ICollection<Project>> DeleteProject(int id) 
        {
            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete == null)
                return NotFound("Project with id " + id + " doesnt exist");
            _context.Remove(projectToDelete);
            _context.SaveChanges();
            return Ok(_context.Projects.Include(x => x.Departments));
        }
    }
}
