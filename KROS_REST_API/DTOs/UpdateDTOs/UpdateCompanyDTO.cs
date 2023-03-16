using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class UpdateCompanyDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DirectorId { get; set; }
    }
}
