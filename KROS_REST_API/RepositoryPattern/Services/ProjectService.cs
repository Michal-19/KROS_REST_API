using KROS_REST_API.Data;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DataContext _context;

        public ProjectService(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> GetOne(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return null;
            return project;
        }

        public async Task<ICollection<Project>?> Add(Project project)
        {
            var division = await _context.Divisions.FindAsync(project.DivisionId);
            if (division == null)
                return null;
            if (project.ProjectChiefId.HasValue)
            {
                var projectChief = await _context.Employees.FindAsync(project.ProjectChiefId);
                if (projectChief == null)
                    return null;
                if (division.CompanyId != projectChief.CompanyWorkId)
                    return null;
            }
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project?> Update(int id, Project project)
        {
            var projectToUpdate = await _context.Projects.FindAsync(id);
            if (projectToUpdate == null)
                return null;
            var division = await _context.Divisions.FindAsync(project.DivisionId);
            if (division == null)
                return null;
            var divisionWithUpdatedDepartments = await _context.Divisions.FindAsync(projectToUpdate.DivisionId);
            if (division.CompanyId != divisionWithUpdatedDepartments.CompanyId)
                return null;
            if (project.ProjectChiefId.HasValue)
            {
                var projectChief = await _context.Employees.FindAsync(project.ProjectChiefId);
                if (projectChief == null)
                    return null;
                if (divisionWithUpdatedDepartments.CompanyId != projectChief.CompanyWorkId)
                    return null;
            }
            projectToUpdate.Name = project.Name;
            projectToUpdate.ProjectChiefId = project.ProjectChiefId;
            projectToUpdate.DivisionId = project.DivisionId;
            await _context.SaveChangesAsync();
            return projectToUpdate;
        }

        public async Task<ICollection<Project>?> Delete(int id)
        {
            var projectToDelete = await _context.Projects.FindAsync(id);
            if (projectToDelete == null)
                return null;
            _context.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            return await _context.Projects.ToListAsync();
        }
    }
}
