using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IDivisionService
    {
        ICollection<Division> GetAll();
        Division? GetOne(int id);
        ICollection<Division>? Add(CreateDivisionDTO division);
        Division? Update(int id, UpdateDivisionDTO division);
        ICollection<Division>? Delete(int id); 
    }
}
