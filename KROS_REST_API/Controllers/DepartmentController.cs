using AutoMapper;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<GetDepartmentDTO>> GetAllDepartments()
        {
            var departmentDTOs = _mapper.Map<ICollection<GetDepartmentDTO>>(_service.GetAll());
            return Ok(departmentDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<GetDepartmentDTO> GetDepartmentById(int id)
        {
            var departmentDTO = _mapper.Map<GetDepartmentDTO>(_service.GetOne(id));
            if (departmentDTO == null)
                return NotFound("Department with id " + id + " doesnt exist!");
            return Ok(departmentDTO);  
        }

        [HttpPost]
        public ActionResult<ICollection<GetDepartmentDTO>> AddDepartment(CreateDepartmentDTO department)
        {
            var newDepartment = _mapper.Map<Department>(department);
            var departmentDTOs = _mapper.Map<ICollection<GetDepartmentDTO>>(_service.Add(newDepartment));
            if (departmentDTOs.IsNullOrEmpty())
                return BadRequest("Wrong filled or empty fields!");
            return Ok(departmentDTOs);
        }

        [HttpPut]
        public ActionResult<GetDepartmentDTO> UpdateDepartment(int id, UpdateDepartmentDTO department)
        {
            var updatedDepartment = _mapper.Map<Department>(department);
            var departmentDTO = _mapper.Map<GetDepartmentDTO>(_service.Update(id, updatedDepartment));
            if (departmentDTO == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(departmentDTO);
        }

        [HttpDelete]
        public ActionResult<ICollection<Department>> DeleteDepartment(int id)
        {
            if (_service.GetOne(id) == null)
                return NotFound("Department with id " + id + " doesnt exist!");
            var departmentDTOs = _mapper.Map<ICollection<GetDepartmentDTO>>(_service.Delete(id));
            return Ok(departmentDTOs);
        }
    }
}
