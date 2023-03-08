﻿using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class DivisionDTO
    {
        [Required]
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
    }
}