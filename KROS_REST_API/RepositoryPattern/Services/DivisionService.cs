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

        public async Task<ICollection<Division>> GetAll()
        {
            return await _context.Divisions.ToListAsync();
        }

        public async Task<Division?> GetOne(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
                return null;
            return division;
        }

        public async Task<ICollection<Division>?> Add(Division division)
        {
            var company = await _context.Companies.FindAsync(division.CompanyId);
            if (company == null)
                return null;
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = await _context.Employees.FindAsync(division.DivisionChiefId);
                if (divisionChief == null)
                    return null;
                if (company.Id != divisionChief.CompanyWorkId)
                    return null;
            }
            await _context.Divisions.AddAsync(division);
            await _context.SaveChangesAsync();
            return await _context.Divisions.ToListAsync();
        }

        public async Task<Division?> Update(int id, Division division)
        {
            var divisionToUpdate = await _context.Divisions.FindAsync(id);
            if (divisionToUpdate == null)
                return null;
            if (division.DivisionChiefId.HasValue)
            {
                var divisionChief = await _context.Employees.FindAsync(division.DivisionChiefId);
                if (divisionChief == null)
                    return null;
                if (divisionToUpdate.CompanyId != divisionChief.CompanyWorkId)
                    return null;
            }
            divisionToUpdate.Name = division.Name;
            divisionToUpdate.DivisionChiefId = division.DivisionChiefId;
            await _context.SaveChangesAsync();
            return divisionToUpdate;
        }

        public async Task<ICollection<Division>?> Delete(int id)
        {
            var divisionToDelete = await _context.Divisions.FindAsync(id);
            if (divisionToDelete == null)
                return null;
            _context.Divisions.Remove(divisionToDelete);
            await _context.SaveChangesAsync();
            return await _context.Divisions.ToListAsync();
        }
    }
}
