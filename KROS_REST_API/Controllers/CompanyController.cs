using AutoMapper;
using KROS_REST_API.DTOs;
using KROS_REST_API.DTOs.CreateDTOs;
using KROS_REST_API.DTOs.GetDTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<GetCompanyDTO>>> GetAllCompanies()
        {
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(await _service.GetAll());
            return Ok(companyDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCompanyDTO>> GetCompanyById(int id)
        {
            var companyDTO = _mapper.Map<GetCompanyDTO>(await _service.GetOne(id));
            if (companyDTO == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(companyDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ICollection<GetCompanyDTO>>> AddCompany(CreateCompanyDTO company)
        {
            var newCompany = _mapper.Map<Company>(company);
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(await _service.Add(newCompany));
            return Ok(companyDTOs);
        }

        [HttpPut]
        public async Task<ActionResult<GetCompanyDTO>> UpdateCompany(int id, UpdateCompanyDTO company)
        {
            var updatedCompany = _mapper.Map<Company>(company);
            var companyDTO = _mapper.Map<GetCompanyDTO>(await _service.Update(id, updatedCompany));
            if (companyDTO == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(companyDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<ICollection<GetCompanyDTO>>> DeleteCompany(int id)
        {
            if (await _service.GetOne(id) == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(await _service.Delete(id));
            return Ok(companyDTOs);
        }
    }
}
