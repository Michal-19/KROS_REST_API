using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Division
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Employee? DivisionChief { get; set; }
        public int? DivisionChiefId { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
