using KROS_REST_API.Data;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _context;

        public CompanyService(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Company>> GetAll()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> GetOne(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return null;
            return company;
        }

        public async Task<ICollection<Company>> Add(Company company)
        {
            await _context.AddAsync(company);
            await _context.SaveChangesAsync();
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company?> Update(int id, Company company)
        {
            var companyToUpdate = await _context.Companies.FindAsync(id);
            if (companyToUpdate == null)
                return null;
            if (company.DirectorId.HasValue)
            {
                var companyDirector = await _context.Employees.FindAsync(company.DirectorId);
                if (companyDirector == null)
                    return null;
                if (companyDirector.CompanyWorkId != id)
                    return null;
            }
            companyToUpdate.Name = company.Name;
            companyToUpdate.DirectorId = company.DirectorId;
            await _context.SaveChangesAsync();
            return companyToUpdate;
        }

        public async Task<ICollection<Company>?> Delete(int id)
        {
            var companyToDelete = await _context.Companies.FindAsync(id);
            if (companyToDelete == null)
                return null;
            if (companyToDelete.Director != null)
                companyToDelete.Director = null;
            var list = await _context.Employees.Where(x => x.CompanyWorkId == id).ToListAsync();
            foreach (var employee in list)
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();
            }
            _context.Remove(companyToDelete);
            _context.SaveChanges();
            return await _context.Companies.ToListAsync();
        }
    }
}
