using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.RepositoryPattern.Interfaces
{
    public interface IProjectService
    {
        ICollection<Project> GetAll();
        Project? GetOne(int id);
        ICollection<Project>? Add(CreateProjectDTO project);
        Project? Update(int id, UpdateProjectDTO project);
        ICollection<Project>? Delete(int id);
    }
}
