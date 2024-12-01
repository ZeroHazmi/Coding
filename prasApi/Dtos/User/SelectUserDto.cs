using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.User
{
    public class SelectUserDto
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string IcNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}