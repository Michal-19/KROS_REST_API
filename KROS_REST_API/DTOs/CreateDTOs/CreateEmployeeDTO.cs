﻿using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs.CreateDTOs
{
    public class CreateEmployeeDTO
    {
        public string? Degree { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public int CompanyWorkId { get; set; }
    }
}
