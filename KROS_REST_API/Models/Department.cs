using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public Employee? DepartmentChief { get; set; }
        public int? DepartmentChiefId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
