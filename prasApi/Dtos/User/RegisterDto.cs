using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using prasApi.Helpers;
using prasApi.Models;

namespace prasApi.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string IcNumber { get; set; } = string.Empty;
        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public string Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Nationality { get; set; } = string.Empty;
        [Required]
        public string Descendants { get; set; } = string.Empty;
        [Required]
        public string Religion { get; set; } = string.Empty;
        public string House_Phone_Number { get; set; } = string.Empty;
        public string Office_Phone_Number { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Postcode { get; set; } = string.Empty;
        [Required]
        public string Region { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
    }
}