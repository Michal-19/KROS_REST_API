using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class UpdateDepartmentDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DepartmentChiefId { get; set; }
    }
}
