using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}