using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using phoneBook.DataAccess.Services.Token;
using phoneBook.Entities.DTO.Account;
using phoneBook.Entities.Models;
using System.Net;

namespace Phonebook.Presentation.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<string>.ValidationError(modelState: ModelState, message: "Validation failed"));

                }

                
                var appUser = new ApplicationUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var isFirstUser = !_userManager.Users.Any();
                var role = isFirstUser ? "Admin" : "User";

                var CreatedUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                // The First User Registerd is Admin otherwise is a usual User
             
                await _userManager.AddToRoleAsync(appUser, role);

                if (CreatedUser.Succeeded)
                {
                    
                    RegisterResponseDTO rgDto = new RegisterResponseDTO
                    {
                        UserName = registerDto.UserName,
                        EmailAddress = registerDto.Email,
                        Token = await _tokenService.CreateToken(appUser)
                    };

                    // Process Done                        
                    return Ok(ApiResponse<RegisterResponseDTO>.Success(data: rgDto, message: "User Created Succsessfully"));                    
                }
                else
                {
                    return StatusCode(500, CreatedUser.Errors);
                }
            }
            catch (Exception ex)
            {
                var errorDetails = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                var errorMessages = new List<string>
                    {
                        $"An error occurred while adding User",
                        $"Error Message: {errorDetails}",
                    };
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<string>.Error("Failed to Add User", errorMessages));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Find User => Then Check password 
            if (!ModelState.IsValid)
            {
                // retuen errors in API Response
                return BadRequest(ApiResponse<string>.ValidationError(modelState: ModelState, message: "Validation failed"));
            }

            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null)
            {
                return Unauthorized(ApiResponse<string>.Error(errors: new List<string> { "Invalid Email Address" }, message: "UnAuthorized Accsess"));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(
                ApiResponse<string>.Error(errors: new List<string> { "UserName Not Found and/or Password Incorrect" }, message: "UnAuthorized Access"));
            }
            RegisterResponseDTO rgDto = new RegisterResponseDTO
            {
                UserName = User.UserName,
                EmailAddress = User.Email,
                Token = await _tokenService.CreateToken(User)
            };

            // Process Done
            var SuccessedLoginResponse = ApiResponse<RegisterResponseDTO>.Success(data: rgDto, message: "User Logged Succsessfully");
            return StatusCode((int)HttpStatusCode.OK, SuccessedLoginResponse);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(ApiResponse<string>.Error("Role name is required"));
            }

            var role = new IdentityRole { Name = roleName };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok(ApiResponse<string>.Success(roleName,$"Role '{roleName}' added successfully"));
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(ApiResponse<string>.Error("Failed to add role", errors));
            }
        }
            
    }
}
