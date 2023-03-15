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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ICollection<Company>> GetAllCompanies()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetCompanyById(int id)
        {
            var company = _service.GetOne(id);
            if (company == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(company);
        }

        [HttpPost]
        public ActionResult<ICollection<Company>> AddCompany(CreateCompanyDTO company)
        {
            return Ok(_service.Add(company));
        }

        [HttpPut]
        public ActionResult<Company> UpdateCompany(int id, UpdateCompanyDTO company)
        {
            var updatedCompany = _service.Update(id, company);
            if (updatedCompany == null)
                return BadRequest("Wrong filled or empty fields!");
            return Ok(updatedCompany);
        }

        [HttpDelete]
        public ActionResult<ICollection<Company>> DeleteCompany(int id)
        {
            var deletedCompany = _service.Delete(id);
            if (deletedCompany == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(deletedCompany);
        }
    }
}
