using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs.CreateDTOs
{
    public class CreateDivisionDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DivisionChiefId { get; set; }
        public int CompanyId { get; set; }
    }
}
