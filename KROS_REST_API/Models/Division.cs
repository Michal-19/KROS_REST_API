using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.Models
{
    public class Division
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public Employee? DivisionChief { get; set; }
        public int? DivisionChiefId { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
