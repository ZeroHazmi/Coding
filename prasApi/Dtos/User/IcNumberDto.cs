using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Dtos.User
{
    public class IcNumberDto
    {
        public string IcNumber { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
    }
}