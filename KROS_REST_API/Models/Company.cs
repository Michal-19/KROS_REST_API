using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Employee? Director { get; set; }
        public int? DirectorId { get; set; }
        public ICollection<Division> Divisions { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
