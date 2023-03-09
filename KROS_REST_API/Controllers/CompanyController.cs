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
        public ActionResult<ICollection<Company>> GetAllCompanies()
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
        public ActionResult<ICollection<Company>> AddCompany(CompanyDTO company)
        {
            if (company.DirectorId.HasValue)
            {
                var employee = _context.Employees.SingleOrDefault(x => x.Id == company.DirectorId);
                if (employee == null)
                    return BadRequest("Employee with id " + company.DirectorId + " doesnt exist!");
            }
            var newCompany = new Company()
            {
                Name = company.Name,
                DirectorId = company.DirectorId
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
                return NotFound("Company with id " + id + " doesnt exist!");
            if (updatedCompany.DirectorId.HasValue)
            {
                var employee = _context.Employees.Find(updatedCompany.DirectorId);
                if (employee == null)
                    return BadRequest("EmployeeId " + updatedCompany.DirectorId + " doesnt exist!");
            }
            companyToUpdate.Name = updatedCompany.Name;
            companyToUpdate.DirectorId = updatedCompany.DirectorId;
            _context.SaveChanges();
            return Ok(companyToUpdate);
        }

        [HttpDelete]
        public ActionResult<ICollection<Company>> DeleteCompany(int id)
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
