using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs.CreateDTOs
{
    public class CreateDepartmentDTO
    {
        [Required]
        public string Name { get; set; }
        public int? DepartmentChiefId { get; set; }
        public int ProjectId { get; set; }
    }
}
