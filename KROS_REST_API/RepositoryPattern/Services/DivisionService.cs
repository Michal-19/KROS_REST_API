using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly DataContext _context;

        public DivisionService(DataContext context)
        {
            _context = context;
        }

        public ICollection<Division> GetAll()
        {
            return _context.Divisions.Include(x => x.Projects).ToList();
        }

        public Division? GetOne(int id)
        {
            var division = _context.Divisions.Include(x => x.Projects).SingleOrDefault(d => d.Id == id);
            if (division == null)
                return null;
            return division;
        }

        public ICollection<Division>? Add(CreateDivisionDTO division)
        {
            var company = _context.Companies.SingleOrDefault(x => x.Id == division.CompanyId);
            if (company == null)
                return null;
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
                if (divisionChief == null)
                    return null;
                if (company.Id != divisionChief.CompanyWorkId)
                    return null;
            }
            var newDivision = new Division()
            {
                Name = division.Name,
                DivisionChiefId = division.DivisionChiefId,
                CompanyId = division.CompanyId
            };
            _context.Divisions.Add(newDivision);
            _context.SaveChanges();
            return _context.Divisions.Include(x => x.Projects).ToList();
        }

        public Division? Update(int id, UpdateDivisionDTO division)
        {
            var divisionToUpdate = _context.Divisions.Include(x => x.Projects).SingleOrDefault(x => x.Id == id);
            if (divisionToUpdate == null)
                return null;
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = _context.Employees.SingleOrDefault(x => x.Id == division.DivisionChiefId);
                if (divisionChief == null)
                    return null;
                if (divisionToUpdate.CompanyId != divisionChief.CompanyWorkId)
                    return null;
            }
            divisionToUpdate.Name = division.Name;
            divisionToUpdate.DivisionChiefId = division.DivisionChiefId;
            _context.SaveChanges();
            return divisionToUpdate;
        }

        public ICollection<Division>? Delete(int id)
        {
            var divisionToDelete = _context.Divisions.Find(id);
            if (divisionToDelete == null)
                return null;
            _context.Divisions.Remove(divisionToDelete);
            _context.SaveChanges();
            return _context.Divisions.Include(x => x.Projects).ToList();
        }
    }
}
