using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface ICompanyService
    {
        ICollection<Company> GetAll();
        Company? GetOne(int id);
        ICollection<Company> Add(Company company);
        Company? Update(int id, Company company);
        ICollection<Company>? Delete(int id);
    }
}
