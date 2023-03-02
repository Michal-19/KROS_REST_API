using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Company>> GetAllCompanies()
        {
            return Ok(_context.Companies);
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetCompanyById(int id)
        {
            var company = _context.Companies.Where(x => x.Id == id).FirstOrDefault();
            if (company == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(company);
        }

        [HttpPost]
        public ActionResult<List<Company>> AddCompany(CompanyDTO company) 
        {
            var employee = _context.Employees.Find(company.EmployeeId);
            if (employee == null)
                return BadRequest();
            var newCompany = new Company()
            {
                Name = company.Name,
                EmployeeId = company.EmployeeId
            };
            _context.Add(newCompany);
            _context.SaveChanges();
            return Ok(_context.Companies);
        }

        [HttpPut]
        public ActionResult<Company> UpdateCompany(int id, CompanyDTO updatedCompany)
        {
            var company = _context.Companies.Where(x => x.Id == id).FirstOrDefault();
            if (company == null)
                return BadRequest("Company with id " + id + " doesnt exist!");
            var employee = _context.Employees.Find(updatedCompany.EmployeeId);
            if (employee == null)
                return BadRequest("EmployeeId " + updatedCompany.EmployeeId + " doesnt exist!");
            company.Name = updatedCompany.Name;
            company.EmployeeId = updatedCompany.EmployeeId;
            _context.SaveChanges();
            return Ok(company);
        }

        [HttpDelete]
        public ActionResult<List<Company>> DeleteCompany(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            _context.Remove(company);
            _context.SaveChanges();
            return Ok(_context.Companies);
        }
    }
}
