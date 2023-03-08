using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public Employee? ProjectChief { get; set; }
        public int? ProjectChiefId { get; set; }
        [JsonIgnore]
        public Division? Division { get; set; }
        public int? DivisionId { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
