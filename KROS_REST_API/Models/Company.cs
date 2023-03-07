using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
