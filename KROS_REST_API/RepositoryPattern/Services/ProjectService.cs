using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
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

        public ICollection<Project> GetAll()
        {
            return _context.Projects.Include(x => x.Departments).ToList();
        }

        public Project? GetOne(int id)
        {
            var project = _context.Projects.Include(x => x.Departments).SingleOrDefault(x => x.Id == id);
            if (project == null)
                return null;
            return project;
        }

        public ICollection<Project>? Add(CreateProjectDTO project)
        {
            var division = _context.Divisions.SingleOrDefault(x => x.Id == project.DivisionId);
            if (division == null)
                return null;
            if (project.ProjectChiefId.HasValue)
            {
                var projectChief = _context.Employees.SingleOrDefault(x => x.Id == project.ProjectChiefId);
                if (projectChief == null)
                    return null;
                if (division.CompanyId != projectChief.CompanyWorkId)
                    return null;
            }
            var newProject = new Project()
            {
                Name = project.Name,
                ProjectChiefId = project.ProjectChiefId,
                DivisionId = project.DivisionId
            };
            _context.Projects.Add(newProject);
            _context.SaveChanges();
            return _context.Projects.ToList();
        }

        public Project? Update(int id, UpdateProjectDTO project)
        {
            var projectToUpdate = _context.Projects.Include(x => x.Departments).SingleOrDefault(x => x.Id == id);
            if (projectToUpdate == null)
                return null;
            var division = _context.Divisions.Find(project.DivisionId);
            if (division == null)
                return null;
            var divisionWithUpdatedDepartments = _context.Divisions.Find(projectToUpdate.DivisionId);
            if (division.CompanyId != divisionWithUpdatedDepartments.CompanyId)
                return null;
            if (project.ProjectChiefId.HasValue)
            {
                var projectChief = _context.Employees.Find(project.ProjectChiefId);
                if (projectChief == null)
                    return null;
                if (divisionWithUpdatedDepartments.CompanyId != projectChief.CompanyWorkId)
                    return null;
            }
            projectToUpdate.Name = project.Name;
            projectToUpdate.ProjectChiefId = project.ProjectChiefId;
            projectToUpdate.DivisionId = project.DivisionId;
            _context.SaveChanges();
            return projectToUpdate;
        }

        public ICollection<Project>? Delete(int id)
        {
            var projectToDelete = _context.Projects.Find(id);
            if (projectToDelete == null)
                return null;
            _context.Remove(projectToDelete);
            _context.SaveChanges();
            return _context.Projects.Include(x => x.Departments).ToList();
        }
    }
}
