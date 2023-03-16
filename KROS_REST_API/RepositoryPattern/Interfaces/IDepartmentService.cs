using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IDepartmentService
    {
        Task<ICollection<Department>> GetAll();
        Task<Department?> GetOne(int id);
        Task<ICollection<Department>?> Add(Department department);
        Task<Department?> Update(int id, Department department);
        Task<ICollection<Department>?> Delete(int id);
    }
}
