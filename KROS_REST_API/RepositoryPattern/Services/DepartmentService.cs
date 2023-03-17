using KROS_REST_API.Data;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetOne(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return null;
            return department;
        }

        public async Task<ICollection<Department>?> Add(Department department)
        {
            var project = await _context.Projects.FindAsync(department.ProjectId);
            if (project == null)
                return null;
            var division = await _context.Divisions.FindAsync(project.DivisionId);
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = await _context.Employees.FindAsync(department.DepartmentChiefId);
                if (departmentChief == null)
                    return null;
                if (division.CompanyId != departmentChief.CompanyWorkId)
                    return null;
            }
            await _context.AddAsync(department);
            await _context.SaveChangesAsync();
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> Update(int id, Department department)
        {
            var departmentToUpdate = await _context.Departments.FindAsync(id);
            if (departmentToUpdate == null)
                return null;
            var project = await _context.Projects.FindAsync(department.ProjectId);
            if (project == null)
                return null;
            var division = await _context.Divisions.FindAsync(project.DivisionId);
            var projectWithUpdatedDepartent = await _context.Projects.FindAsync(departmentToUpdate.ProjectId);
            var divisionWithUpdatedDepartment = await _context.Divisions.FindAsync(projectWithUpdatedDepartent.DivisionId);
            if (division.CompanyId != divisionWithUpdatedDepartment.CompanyId)
                return null;
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = await _context.Employees.FindAsync(department.DepartmentChiefId);
                if (departmentChief == null)
                    return null;
                if (departmentChief.CompanyWorkId != divisionWithUpdatedDepartment.CompanyId)
                    return null;
            }
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.DepartmentChiefId = department.DepartmentChiefId;
            departmentToUpdate.ProjectId = department.ProjectId;
            await _context.SaveChangesAsync();
            return departmentToUpdate;
        }

        public async Task<ICollection<Department>?> Delete(int id)
        {
            var departmentToDelete = await _context.Departments.FindAsync(id);
            if (departmentToDelete == null)
                return null;
            _context.Remove(departmentToDelete);
            await _context.SaveChangesAsync();
            return await _context.Departments.ToListAsync();
        }
    }
}
