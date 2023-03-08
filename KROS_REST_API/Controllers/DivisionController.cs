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
            return _context.Divisions.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Division> GetDivisionById(int id)
        {
            var division = _context.Divisions.SingleOrDefault(d => d.Id == id);
            if (division == null)
                return NotFound("Division with id " + id + " doesnt exist!");
            return Ok(division);
        }

        [HttpPost]
        public ActionResult<ICollection<Division>> AddDivision(DivisionDTO division)
        {
            var employee = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
            if (employee == null)
                return BadRequest("Wrong or missing EmployeeId value");
            var company = _context.Companies.SingleOrDefault(x => x.Id == division.CompanyId);
            if (company == null)
                return BadRequest("Wrong or missing CompanyId value");
            var newDivision = new Division()
            {
                Name = division.Name,
                DivisionChiefId = division.DivisionChiefId,
                CompanyId = division.CompanyId
            };
            _context.Divisions.Add(newDivision);
            _context.SaveChanges();
            return Ok(_context.Divisions);
        }

        [HttpPut]
        public ActionResult<Division> UpdateDivision(int id, DivisionDTO division)
        {
            var divisionToUpdate = _context.Divisions.SingleOrDefault(x => x.Id == id);
            if (divisionToUpdate == null)
                return BadRequest("Division with id " + id + " doesnt exist!");
            var employee = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
            if (employee == null)
                return BadRequest("Wrong or missing EmployeeId value");
            var company = _context.Companies.SingleOrDefault(x => x.Id == division.CompanyId);
            if (company == null)
                return BadRequest("Wrong or missing CompanyId value");
            divisionToUpdate.Name = division.Name;
            divisionToUpdate.DivisionChiefId = division.DivisionChiefId;
            divisionToUpdate.CompanyId = division.CompanyId;
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
            return Ok(_context.Divisions);
        }
    }
}
