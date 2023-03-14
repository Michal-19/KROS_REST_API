using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DivisionController : ControllerBase
    {
        private readonly IDivisionService _service;
        public DivisionController(IDivisionService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ICollection<Division>> GetAllDivisions() 
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Division> GetDivisionById(int id)
        {
            var division = _service.GetOne(id);
            if (division == null)
                return NotFound();
            return Ok(division);
        }

        [HttpPost]
        public ActionResult<ICollection<Division>> AddDivision(CreateDivisionDTO division)
        {
            var addedDivision = _service.Add(division);
            if (addedDivision == null)
                return BadRequest();
            return Ok(addedDivision);
        }

        [HttpPut]
        public ActionResult<Division> UpdateDivision(int id, UpdateDivisionDTO division)
        {
            var updatedDivision = _service.Update(id, division);
            if (updatedDivision == null)
                return BadRequest();
            return Ok(updatedDivision);
        }

        [HttpDelete]
        public ActionResult<ICollection<Division>> DeleteDivision(int id)
        {
            var deletedDivision = _service.Delete(id);
            if (deletedDivision == null)
                return NotFound();
            return Ok(deletedDivision);
        }
    }
}
