using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Police
{
    public class PoliceDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? IcNumber { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public Gender Gender { get; set; }
    }
}