﻿using System.ComponentModel.DataAnnotations;

namespace KROS_REST_API.DTOs
{
    public class CreateCompanyDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
