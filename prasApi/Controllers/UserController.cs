using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prasApi.Dtos.User;
using prasApi.Interfaces;
using prasApi.Models;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Return the user details (you can customize the fields as needed)
            var userDto = new UserDto
            {
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                IcNumber = user.IcNumber,
                Birthday = user.Birthday.ToString(),
                Gender = user.Gender,
                Nationality = user.Nationality,
                Descendants = user.Descendants,
                Religion = user.Religion,
                PhoneNumber = user.PhoneNumber,
                House_Phone_Number = user.HousePhoneNumber,
                Office_Phone_Number = user.OfficePhoneNumber,
                Address = user.Address,
                Postcode = user.Postcode,
                Region = user.Region,
                State = user.State
            };

            return Ok(userDto);
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers([FromQuery] string? search, [FromQuery] string? gender)
        {
            // Retrieve all users
            var users = await _userManager.Users.ToListAsync();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                users = users.Where(user =>
                    (!string.IsNullOrEmpty(user.UserName) && user.UserName.ToLower().Contains(search)) ||
                    (!string.IsNullOrEmpty(user.Name) && user.Name.ToLower().Contains(search))
                ).ToList();
            }

            // Apply gender filter if provided
            if (!string.IsNullOrWhiteSpace(gender) && gender.ToLower() != "all")
            {
                // Parse the gender string into the Gender enum
                if (Enum.TryParse(typeof(Gender), gender, true, out var genderEnum))
                {
                    users = users.Where(user => user.Gender == (Gender)genderEnum).ToList();
                }
                else
                {
                    return BadRequest($"Invalid gender value: {gender}");
                }
            }

            // Map users to a DTO for frontend compatibility
            var userDtos = users.Select(user => new
            {
                id = user.Id,               // Match TypeScript `id` field
                username = user.UserName,   // Match TypeScript `username` field
                name = user.Name,           // Match TypeScript `name` field
                icNumber = user.IcNumber,   // Match TypeScript `icNumber` field
                email = user.Email,         // Match TypeScript `email` field
                age = DateTime.Now.Year - user.Birthday.Year, // Calculate age
                gender = user.Gender        // Match TypeScript `gender` field
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("getIcNumber/{IcNumber}")]
        public async Task<IActionResult> GetUserByIcNumber(string IcNumber)
        {
            if (string.IsNullOrEmpty(IcNumber))
            {
                return BadRequest("IC Number is required.");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.IcNumber == IcNumber);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Return the user details (you can customize the fields as needed)
            var userDto = new IcNumberDto
            {
                userId = user.Id
            };
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized(new { Success = false, Message = "Invalid username" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            Console.WriteLine($"Result: Succeeded = {result.Succeeded}, IsLockedOut = {result.IsLockedOut}, IsNotAllowed = {result.IsNotAllowed}, RequiresTwoFactor = {result.RequiresTwoFactor}");

            if (!result.Succeeded)
            {
                return Unauthorized(new { Success = false, Message = "Username not found and/or password incorrect" });
            }

            var Token = await _tokenService.CreateToken(user);
            Console.WriteLine($"Token: {Token}");


            return Ok(
                new NewUserDto()
                {
                    UserName = user.UserName,
                    Token = Token
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(new { message = "Validation failed", errors });
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    IcNumber = registerDto.IcNumber,
                    Birthday = DateOnly.Parse(registerDto.Birthday),
                    Gender = registerDto.Gender,
                    Nationality = registerDto.Nationality,
                    Descendants = registerDto.Descendants,
                    Religion = registerDto.Religion,
                    PhoneNumber = registerDto.PhoneNumber,
                    HousePhoneNumber = registerDto.House_Phone_Number,
                    OfficePhoneNumber = registerDto.Office_Phone_Number,
                    Address = registerDto.Address,
                    Postcode = registerDto.Postcode,
                    Region = registerDto.Region,
                    State = registerDto.State
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto()
                            {
                                UserName = appUser.UserName,
                                Token = await _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromQuery] string username, [FromQuery] string password, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var updatedUser = updateUserDto.ToAppUserFromUpdateUserDto(user, updateUserDto);

            // If a new password is provided, change the password
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, password, updateUserDto.Password);

                if (!changePasswordResult.Succeeded)
                {
                    return StatusCode(500, changePasswordResult.Errors);
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }
            return NoContent();
        }
    }
}