using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IDepartmentService
    {
        ICollection<Department> GetAll();
        Department? GetOne(int id);
        ICollection<Department>? Add(Department department);
        Department? Update(int id, Department department);
        ICollection<Department>? Delete(int id);
    }
}
