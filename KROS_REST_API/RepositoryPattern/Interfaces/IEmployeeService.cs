using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IEmployeeService
    {
        ICollection<Employee> GetAll();
        Employee? GetOne(int id);
        ICollection<Employee>? Add(CreateEmployeeDTO employee);
        Employee? Update(int id, UpdateEmployeeDTO employee);
        ICollection<Employee>? Delete(int id);
    }
}
