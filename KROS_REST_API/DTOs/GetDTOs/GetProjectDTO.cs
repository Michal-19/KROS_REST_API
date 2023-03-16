namespace KROS_REST_API.DTOs.GetDTOs
{
    public class GetProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectChiefId { get; set; }
        public int DivisionId { get; set; }
    }
}
