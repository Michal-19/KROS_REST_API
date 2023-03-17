using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IEmployeeService
    {
        Task<ICollection<Employee>> GetAll();
        Task<Employee?> GetOne(int id);
        Task<ICollection<Employee>?> Add(Employee employee);
        Task<Employee?> Update(int id, Employee employee);
        Task<ICollection<Employee>?> Delete(int id);
    }
}
