using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IDivisionService
    {
        Task<ICollection<Division>> GetAll();
        Task<Division?> GetOne(int id);
        Task<ICollection<Division>?> Add(Division division);
        Task<Division?> Update(int id, Division division);
        Task<ICollection<Division>?> Delete(int id); 
    }
}
