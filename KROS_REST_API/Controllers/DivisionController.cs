using AutoMapper;
using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionService _service;
        private readonly IMapper _mapper;

        public DivisionController(IDivisionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<GetDivisionDTO>> GetAllDivisions() 
        {
            var divisionDTOs = _mapper.Map<ICollection<GetDivisionDTO>>(_service.GetAll());
            return Ok(divisionDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<GetDivisionDTO> GetDivisionById(int id)
        {
            var divisionDTO = _mapper.Map<GetDivisionDTO>(_service.GetOne(id));
            if (divisionDTO == null)
                return NotFound("Division with id " + id + " doesnt exist!");
            return Ok(divisionDTO);
        }

        [HttpPost]
        public ActionResult<ICollection<GetDivisionDTO>> AddDivision(CreateDivisionDTO division)
        {
            var newDivision = _mapper.Map<Division>(division);
            var divisionDTOs = _mapper.Map<ICollection<GetDivisionDTO>>(_service.Add(newDivision));
            if (divisionDTOs.IsNullOrEmpty())
                return BadRequest("Wrong filled or empty fields!");
            return Ok(divisionDTOs);
        }

        [HttpPut]
        public ActionResult<GetDivisionDTO> UpdateDivision(int id, UpdateDivisionDTO division)
        {
            var updatedDivision = _mapper.Map<Division>(division);
            var divisionDTO = _mapper.Map<GetDivisionDTO>(_service.Update(id, updatedDivision));
            if (divisionDTO == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(divisionDTO);
        }

        [HttpDelete]
        public ActionResult<ICollection<GetDivisionDTO>> DeleteDivision(int id)
        {
            if (_service.GetOne(id) == null)
                return NotFound("Division with id " + id + " doesnt exist!");
            var divisionDTOs = _mapper.Map<ICollection<GetDivisionDTO>>(_service.Delete(id));
            return Ok(divisionDTOs);
        }
    }
}
