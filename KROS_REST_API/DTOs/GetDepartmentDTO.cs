namespace KROS_REST_API.DTOs
{
    public class GetDepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DepartmentChiefId { get; set; }
        public int ProjectId { get; set; }
    }
}
