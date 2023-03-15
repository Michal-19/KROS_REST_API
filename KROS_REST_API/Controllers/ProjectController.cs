﻿using AutoMapper;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<GetProjectDTO>> GetAllProjects()
        {
            var projectDTOs = _mapper.Map<ICollection<GetProjectDTO>>(_service.GetAll());
            return Ok(projectDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<GetProjectDTO> GetProjectById(int id)
        {
            var projectDTO = _mapper.Map<GetProjectDTO>(_service.GetOne(id));
            if (projectDTO == null)
                return NotFound("Project with id " + id + " doesnt exist!");
            return Ok(projectDTO);
        }

        [HttpPost]
        public ActionResult<ICollection<GetProjectDTO>> AddProject(CreateProjectDTO project)
        {
            var newProject = _mapper.Map<Project>(project);
            var projectDTOs = _mapper.Map<ICollection<GetProjectDTO>>(_service.Add(newProject)); 
            if (projectDTOs.IsNullOrEmpty())
                return BadRequest("Wrong filled or empty fields!");
            return Ok(projectDTOs);
        }

        [HttpPut]
        public ActionResult<GetProjectDTO> UpdateProject(int id, UpdateProjectDTO project)
        {
            var updatedProject = _mapper.Map<Project>(project);
            var projectDTOs = _mapper.Map<GetProjectDTO>(_service.Update(id, updatedProject));
            if (projectDTOs == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(projectDTOs);
        }

        [HttpDelete]
        public ActionResult<ICollection<GetProjectDTO>> DeleteProject(int id) 
        {
            if (_service.GetOne(id) == null)
                return NotFound("Project with id " + id + " doesnt exist!");
            var projectDTOs = _mapper.Map<ICollection<GetProjectDTO>>(_service.Delete(id));
            return Ok(projectDTOs);
        }
    }
}
