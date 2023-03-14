using KROS_REST_API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KROS_REST_API.DTOs
{
    public class UpdateCompanyDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DirectorId { get; set; }
    }
}
