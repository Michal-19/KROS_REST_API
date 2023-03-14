using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionController : ControllerBase
    {
        private readonly DataContext _context;
        public DivisionController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<ICollection<Division>> GetAllDivisions() 
        {
            return Ok(_context.Divisions.Include(x => x.Projects));
        }

        [HttpGet("{id}")]
        public ActionResult<Division> GetDivisionById(int id)
        {
            var division = _context.Divisions.Include(x => x.Projects).SingleOrDefault(d => d.Id == id);
            if (division == null)
                return NotFound("Division with id " + id + " doesnt exist!");
            return Ok(division);
        }

        [HttpPost]
        public ActionResult<ICollection<Division>> AddDivision(CreateDivisionDTO division)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == division.CompanyId);
            if (company == null)
                return BadRequest("Wrong filled or missing companyId field!");
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
                if (divisionChief == null)
                    return BadRequest("Employee with id " + division.DivisionChiefId + " doesnt exist!");
                if (company.Id != divisionChief.CompanyWorkId)
                    return BadRequest("Employee with id " + divisionChief.Id + " work in other company!");

            }
            var newDivision = new Division()
            {
                Name = division.Name,
                DivisionChiefId = division.DivisionChiefId,
                CompanyId = division.CompanyId
            };
            _context.Divisions.Add(newDivision);
            _context.SaveChanges();
            return Ok(_context.Divisions.Include(x => x.Projects));
        }

        [HttpPut]
        public ActionResult<Division> UpdateDivision(int id, UpdateDivisionDTO division)
        {
            var divisionToUpdate = _context.Divisions.Include(x => x.Projects).SingleOrDefault(x => x.Id == id);
            if (divisionToUpdate == null)
                return NotFound("Division with id " + id + " doesnt exist!");
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
                if (divisionChief == null)
                    return BadRequest("Wrong filled EmployeeId field");
                if (divisionToUpdate.CompanyId != divisionChief.CompanyWorkId)
                    return BadRequest("Employee with id " + divisionChief.Id + " work in other cmpany!");
            }
            divisionToUpdate.Name = division.Name;
            divisionToUpdate.DivisionChiefId = division.DivisionChiefId;
            _context.SaveChanges();
            return Ok(divisionToUpdate);
        }

        [HttpDelete]
        public ActionResult<ICollection<Division>> DeleteDivision(int id)
        {
            var divisionToDelete = _context.Divisions.Find(id);
            if (divisionToDelete == null) 
                return NotFound("Division with id" + id + " doesnt exist");
            _context.Divisions.Remove(divisionToDelete);
            _context.SaveChanges();
            return Ok(_context.Divisions.Include(x => x.Projects));
        }
    }
}
