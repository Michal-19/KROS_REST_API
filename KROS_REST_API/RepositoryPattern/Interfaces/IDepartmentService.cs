using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IDepartmentService
    {
        ICollection<Department> GetAll();
        Department? GetOne(int id);
        ICollection<Department>? Add(CreateDepartmentDTO department);
        Department? Update(int id, UpdateDepartmentDTO department);
        ICollection<Department>? Delete(int id);
    }
}
