using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class DepartmentDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DepartmentChiefId { get; set; }
        public int? ProjectId { get; set; }
    }
}
