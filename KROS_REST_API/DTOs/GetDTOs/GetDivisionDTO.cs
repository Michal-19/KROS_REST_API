namespace KROS_REST_API.DTOs.GetDTOs
{
    public class GetDivisionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DivisionChiefId { get; set; }
        public int CompanyId { get; set; }
    }
}
