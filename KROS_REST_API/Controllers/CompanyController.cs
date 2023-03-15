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
        public ActionResult<ICollection<GetCompanyDTO>> GetAllCompanies()
        {
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(_service.GetAll());
            return Ok(companyDTOs);
        }

        [HttpGet("{id}")]
        public ActionResult<GetCompanyDTO> GetCompanyById(int id)
        {
            var companyDTO = _mapper.Map<GetCompanyDTO>(_service.GetOne(id));
            if (companyDTO == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(companyDTO);
        }

        [HttpPost]
        public ActionResult<ICollection<GetCompanyDTO>> AddCompany(CreateCompanyDTO company)
        {
            var newCompany = _mapper.Map<Company>(company);
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(_service.Add(newCompany));
            return Ok(companyDTOs);
        }

        [HttpPut]
        public ActionResult<GetCompanyDTO> UpdateCompany(int id, UpdateCompanyDTO company)
        {
            var updatedCompany = _mapper.Map<Company>(company);
            var companyDTO = _mapper.Map<GetCompanyDTO>(_service.Update(id, updatedCompany));
            if (companyDTO == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(companyDTO);
        }

        [HttpDelete]
        public ActionResult<ICollection<Company>> DeleteCompany(int id)
        {
            var companyDTOs = _mapper.Map<ICollection<GetCompanyDTO>>(_service.Delete(id));
            if (companyDTOs.IsNullOrEmpty())
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(companyDTOs);
        }
    }
}
