using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prasApi.Dtos.Police;
using prasApi.Dtos.User;
using prasApi.Interfaces;
using prasApi.Models;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/police")]
    public class PoliceController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public PoliceController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(PoliceLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto()
                {
                    UserName = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PoliceCreateDto registerDto)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    IcNumber = registerDto.IcNumber,
                    Gender = registerDto.Gender

                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Police");

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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string userId)
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Get all users with the "Police" role
                var users = await _userManager.Users.ToListAsync();
                var policeUsers = users
                    .Where(u => _userManager.GetRolesAsync(u).Result.Contains("Police"))
                    .ToList();

                if (policeUsers == null || !policeUsers.Any())
                {
                    return NotFound("No police users found.");
                }

                // Map the users to a list of PoliceDto
                var policeDtos = policeUsers.Select(u => new PoliceDto
                {
                    Username = u.UserName,
                    Email = u.Email,
                    IcNumber = u.IcNumber,
                    // You can add more properties if needed
                }).ToList();

                return Ok(policeDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}