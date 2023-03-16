using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface ICompanyService
    {
        Task<ICollection<Company>> GetAll();
        Task<Company?> GetOne(int id);
        Task<ICollection<Company>> Add(Company company);
        Task<Company?> Update(int id, Company company);
        Task<ICollection<Company>?> Delete(int id);
    }
}
