using AutoMapper;
using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<GetEmployeeDTO>>> GetAllEmployees()
        {
            var employeesDTOs = _mapper.Map<ICollection<GetEmployeeDTO>>(await _service.GetAll());
            return Ok(employeesDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployeeById(int id)
        {
            var employeeDTO = _mapper.Map<GetEmployeeDTO>(await _service.GetOne(id));
            if (employeeDTO == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            return Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<GetEmployeeDTO>>> AddEmployee(CreateEmployeeDTO employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            var employeeDTOs = _mapper.Map<ICollection<GetEmployeeDTO>>(await _service.Add(newEmployee));
            if (employeeDTOs.IsNullOrEmpty())
                return BadRequest("Company with id " + employee.CompanyWorkId + " doesnt exist!");
            return Ok(employeeDTOs);
        }

        [HttpPut]
        public async Task<ActionResult<GetEmployeeDTO>> Update(int id, UpdateEmployeeDTO employee)
        {
            var updatedEmployee = _mapper.Map<Employee>(employee);
            var employeeDTO = _mapper.Map<GetEmployeeDTO>(await _service.Update(id, updatedEmployee));
            if (employeeDTO == null)
                return BadRequest("Employee with id " + id + " doesnt exist!");
            return Ok(employeeDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<ICollection<GetEmployeeDTO>>> Delete(int id)
        {
            if (await _service.GetOne(id) == null)
                return NotFound("Employee with id " + id + " doesnt exist!");
            var employeeDTOs = _mapper.Map<ICollection<GetEmployeeDTO>>(await _service.Delete(id));
            return Ok(employeeDTOs);
        }
    }
}
