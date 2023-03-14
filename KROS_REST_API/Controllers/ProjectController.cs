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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ICollection<Project>> GetAllProjects()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProjectById(int id)
        {
            var project = _service.GetOne(id);
            if (project == null)
                return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public ActionResult<ICollection<Project>> AddProject(CreateProjectDTO project)
        {
            var addedProject = _service.Add(project);
            if (addedProject == null)
                return BadRequest();
            return Ok(addedProject);
        }

        [HttpPut]
        public ActionResult<Project> UpdateProject(int id, UpdateProjectDTO project)
        {
            var updatedProject = _service.Update(id, project);
            if (updatedProject == null)
                return BadRequest();
            return Ok(updatedProject);
        }

        [HttpDelete]
        public ActionResult<ICollection<Project>> DeleteProject(int id) 
        {
            var deletedProject = _service.Delete(id);
            if (deletedProject == null)
                return NotFound();
            return Ok(deletedProject);
        }
    }
}
