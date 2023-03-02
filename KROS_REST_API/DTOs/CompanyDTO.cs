using KROS_REST_API.Models;
using System.Text.Json.Serialization;

namespace KROS_REST_API.DTOs
{
    public class CompanyDTO
    {
        public string Name { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
    }
}
