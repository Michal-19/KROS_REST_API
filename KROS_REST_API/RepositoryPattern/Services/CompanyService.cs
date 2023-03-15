using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
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

        public ICollection<Company> GetAll()
        {
            return _context.Companies.ToList();
        }

        public Company? GetOne(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
                return null;
            return company;
        }

        public ICollection<Company> Add(Company company)
        {
            _context.Add(company);
            _context.SaveChanges();
            return _context.Companies.ToList();
        }

        public Company? Update(int id, Company company)
        {
            var companyToUpdate = _context.Companies.Find(id);
            if (companyToUpdate == null)
                return null;
            if (company.DirectorId.HasValue)
            {
                var companyDirector = _context.Employees.Find(company.DirectorId);
                if (companyDirector == null)
                    return null;
                if (companyDirector.CompanyWorkId != id)
                    return null;
            }
            companyToUpdate.Name = company.Name;
            companyToUpdate.DirectorId = company.DirectorId;
            _context.SaveChanges();
            return companyToUpdate;
        }

        public ICollection<Company>? Delete(int id)
        {
            var companyToDelete = _context.Companies.Find(id);
            if (companyToDelete == null)
                return null;
            if (companyToDelete.Director != null)
                companyToDelete.Director = null;
            var list = _context.Employees.Where(x => x.CompanyWorkId == id).ToList();
            foreach (var employee in list)
            {
                _context.Remove(employee);
                _context.SaveChanges();
            }
            _context.Remove(companyToDelete);
            _context.SaveChanges();
            return _context.Companies.Include(x => x.Divisions).ToList();
        }
    }
}
