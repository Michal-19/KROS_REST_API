using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs.CreateDTOs
{
    public class CreateProjectDTO
    {
        [Required]
        public string Name { get; set; }
        public int? ProjectChiefId { get; set; }
        public int DivisionId { get; set; }
    }
}
