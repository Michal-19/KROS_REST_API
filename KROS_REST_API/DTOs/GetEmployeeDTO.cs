namespace KROS_REST_API.DTOs
{
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public string? Degree { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public int CompanyWorkId { get; set; }
    }
}
