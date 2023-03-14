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
        public ActionResult<ICollection<Company>> AddCompany(CreateCompanyDTO company)
        {
            var newCompany = new Company()
            {
                Name = company.Name
            };
            _context.Add(newCompany);
            _context.SaveChanges();
            return Ok(_context.Companies.Include(x => x.Divisions));
        }

        [HttpPut]
        public ActionResult<Company> UpdateCompany(int id, UpdateCompanyDTO updatedCompany)
        {
            var companyToUpdate = _context.Companies.Include(x => x.Divisions).SingleOrDefault(x => x.Id == id);
            if (companyToUpdate == null)
                return NotFound("Company with id " + id + " doesnt exist!");
            if (updatedCompany.DirectorId.HasValue)
            {
                var companyDirector = _context.Employees.Find(updatedCompany.DirectorId);
                if (companyDirector == null)
                    return BadRequest("Employee with id " + updatedCompany.DirectorId + " doesnt exist!");
                if (companyDirector.CompanyWorkId != id)
                    return BadRequest("Employee with id " + companyDirector.Id + " work in other company!");
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
            if (company.Director != null)
                company.Director = null;
            var list = _context.Employees.Where(x => x.CompanyWorkId== id).ToList();
            foreach (var employee in list)
            {
                _context.Remove(employee);
                _context.SaveChanges();
            }
            _context.Remove(company);
            _context.SaveChanges();
            return Ok(_context.Companies.Include(x => x.Divisions));
        }
    }
}
