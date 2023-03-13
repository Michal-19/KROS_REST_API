using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class UpdateProjectDTO
    {
        [Required]
        public string Name { get; set; }
        public int? ProjectChiefId { get; set; }
    }
}
