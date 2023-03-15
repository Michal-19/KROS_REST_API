using KROS_REST_API.Data;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;
using KROS_REST_API.RepositoryPattern.Interfaces;

namespace KROS_REST_API.RepositoryPattern.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public ICollection<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department? GetOne(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
                return null;
            return department;
        }

        public ICollection<Department>? Add(Department department)
        {
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return null;
            var division = _context.Divisions.Find(project.DivisionId);
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = _context.Employees.Find(department.DepartmentChiefId);
                if (departmentChief == null)
                    return null;
                if (division.CompanyId != departmentChief.CompanyWorkId)
                    return null;
            }
            _context.Add(department);
            _context.SaveChanges();
            return _context.Departments.ToList();
        }

        public Department? Update(int id, Department department)
        {
            var departmentToUpdate = _context.Departments.Find(id);
            if (departmentToUpdate == null)
                return null;
            var project = _context.Projects.Find(department.ProjectId);
            if (project == null)
                return null;
            var division = _context.Divisions.Find(project.DivisionId);
            var projectWithUpdatedDepartent = _context.Projects.Find(departmentToUpdate.ProjectId);
            var divisionWithUpdatedDepartment = _context.Divisions.Find(projectWithUpdatedDepartent.DivisionId);
            if (division.CompanyId != divisionWithUpdatedDepartment.CompanyId)
                return null;
            if (department.DepartmentChiefId.HasValue)
            {
                var departmentChief = _context.Employees.Find(department.DepartmentChiefId);
                if (departmentChief == null)
                    return null;
                if (departmentChief.CompanyWorkId != divisionWithUpdatedDepartment.CompanyId)
                    return null;
            }
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.DepartmentChiefId = department.DepartmentChiefId;
            departmentToUpdate.ProjectId = department.ProjectId;
            _context.SaveChanges();
            return departmentToUpdate;
        }

        public ICollection<Department>? Delete(int id)
        {
            var departmentToDelete = _context.Departments.Find(id);
            if (departmentToDelete == null)
                return null;
            _context.Remove(departmentToDelete);
            _context.SaveChanges();
            return _context.Departments.ToList();
        }
    }
}
