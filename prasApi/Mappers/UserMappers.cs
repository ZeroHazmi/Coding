using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Dtos.User;
using prasApi.Models;

namespace prasApi.Mappers
{
    public static class UserMappers
    {
        public static AppUser ToAppUserFromUpdateUserDto(AppUser appUser, UpdateUserDto updateUserDto)
        {
            return new AppUser
            {
                UserName = updateUserDto.Username ?? appUser.UserName,
                Email = updateUserDto.Email ?? appUser.Email,
                PhoneNumber = updateUserDto.PhoneNumber ?? appUser.PhoneNumber,
                IcNumber = updateUserDto.IcNumber ?? appUser.IcNumber,
                Birthday = updateUserDto.Birthday != null ? DateOnly.Parse(updateUserDto.Birthday) : appUser.Birthday,
                Gender = updateUserDto.Gender == appUser.Gender ? appUser.Gender : updateUserDto.Gender,
                Nationality = updateUserDto.Nationality ?? appUser.Nationality,
                Descendants = updateUserDto.Descendants ?? appUser.Descendants,
                Religion = updateUserDto.Religion ?? appUser.Religion,
                HousePhoneNumber = updateUserDto.House_Phone_Number ?? appUser.HousePhoneNumber,
                OfficePhoneNumber = updateUserDto.Office_Phone_Number ?? appUser.OfficePhoneNumber,
                Address = updateUserDto.Address ?? appUser.Address,
                Postcode = updateUserDto.Postcode ?? appUser.Postcode,
                Region = updateUserDto.Region ?? appUser.Region,
                State = updateUserDto.State ?? appUser.State
            };
        }
    }
}