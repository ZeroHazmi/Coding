using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.User
{
    public class UserDto
    {
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string IcNumber { get; set; } = string.Empty;
        public string Birthday { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Descendants { get; set; } = string.Empty;
        public string Religion { get; set; } = string.Empty;
        public string House_Phone_Number { get; set; } = string.Empty;
        public string Office_Phone_Number { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        internal AppUser ToAppUserFromUpdateUserDto(AppUser user, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}