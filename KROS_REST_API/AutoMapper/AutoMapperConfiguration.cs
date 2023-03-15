using AutoMapper;
using KROS_REST_API.DTOs;
using KROS_REST_API.Models;

namespace KROS_REST_API.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Employee, GetEmployeeDTO>();
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
            CreateMap<Company, GetCompanyDTO>();
            CreateMap<CreateCompanyDTO, Company>();
            CreateMap<UpdateCompanyDTO, Company>();
            CreateMap<Division, GetDivisionDTO>();
            CreateMap<GetDivisionDTO, Division>();
            CreateMap<CreateDivisionDTO, Division>();
            CreateMap<UpdateDivisionDTO, Division>();
            CreateMap<Project, GetProjectDTO>();
            CreateMap<CreateProjectDTO, Project>();
            CreateMap<UpdateProjectDTO, Project>();
            CreateMap<Department, GetDepartmentDTO>();
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>();
        }
    }
}
