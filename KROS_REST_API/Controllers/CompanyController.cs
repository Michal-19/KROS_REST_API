using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return Ok(_context.Companies.Include(x => x.Divisions));
        }

        [HttpGet("{id}")]
        public ActionResult<Company> GetCompanyById(int id)
        {
            var company = _context.Companies.Include(x => x.Divisions).SingleOrDefault(x => x.Id == id);
            if (company == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            return Ok(company);
        }

        [HttpPost]
        public ActionResult<List<Company>> AddCompany(CompanyDTO company)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == company.EmployeeId);
            if (employee == null)
                return BadRequest("Employee with id " + company.EmployeeId + " doesnt exist!");
            var newCompany = new Company()
            {
                Name = company.Name,
                EmployeeId = company.EmployeeId
            };
            _context.Add(newCompany);
            _context.SaveChanges();
            return Ok(_context.Companies.Include(x => x.Divisions));
        }

        [HttpPut]
        public ActionResult<Company> UpdateCompany(int id, CompanyDTO updatedCompany)
        {
            var companyToUpdate = _context.Companies.Include(x => x.Divisions).SingleOrDefault(x => x.Id == id);
            if (companyToUpdate == null)
                return BadRequest("Company with id " + id + " doesnt exist!");
            var employee = _context.Employees.Find(updatedCompany.EmployeeId);
            if (employee == null)
                return BadRequest("EmployeeId " + updatedCompany.EmployeeId + " doesnt exist!");
            companyToUpdate.Name = updatedCompany.Name;
            companyToUpdate.EmployeeId = updatedCompany.EmployeeId;
            _context.SaveChanges();
            return Ok(companyToUpdate);
        }

        [HttpDelete]
        public ActionResult<List<Company>> DeleteCompany(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            _context.Remove(company);
            _context.SaveChanges();
            return Ok(_context.Companies.Include(x => x.Divisions));
        }
    }
}
