using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace prasApi.Models
{
    public class AppUser : IdentityUser
    {
        // Properties specific to User Only
        public string IcNumber { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Descendants { get; set; } = string.Empty;
        public string Religion { get; set; } = string.Empty;
        public string HousePhoneNumber { get; set; } = string.Empty;
        public string OfficePhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        // Navigation Properties
        public List<Report> Reports { get; set; } = new List<Report>();
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}