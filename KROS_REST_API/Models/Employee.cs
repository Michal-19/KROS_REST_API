﻿namespace KROS_REST_API.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Company> Companies { get; set; }
    }
}
