using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class UpdateDivisionDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DivisionChiefId { get; set; }
    }
}
