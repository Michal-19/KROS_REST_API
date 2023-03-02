using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
