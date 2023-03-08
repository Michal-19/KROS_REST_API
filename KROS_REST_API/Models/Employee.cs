using System.ComponentModel.DataAnnotations;

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
        public ICollection<Company> Companies { get; set; }
        public ICollection<Division> Divisions { get; set; }
    }
}
