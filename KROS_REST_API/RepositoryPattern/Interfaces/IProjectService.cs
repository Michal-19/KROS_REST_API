using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IProjectService
    {
        Task<ICollection<Project>> GetAll();
        Task<Project?> GetOne(int id);
        Task<ICollection<Project>?> Add(Project project);
        Task<Project?> Update(int id, Project project);
        Task<ICollection<Project>?> Delete(int id);
    }
}
