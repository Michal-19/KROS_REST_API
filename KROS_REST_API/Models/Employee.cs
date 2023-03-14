using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Degree { get; set; }
        [Required]  
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public Company CompanyWork { get; set; }
        public int CompanyWorkId { get; set; }
        public ICollection<Company> CompaniesChief { get; set; }
        public ICollection<Division> DivisionsChief { get; set; }
        public ICollection<Project> ProjectsChief { get; set; }
        public ICollection<Department> DepartmentsChief { get; set; }
    }
}
