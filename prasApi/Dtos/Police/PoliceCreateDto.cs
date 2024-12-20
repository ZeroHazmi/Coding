using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Police
{
    public class PoliceCreateDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string IcNumber { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
    }
}