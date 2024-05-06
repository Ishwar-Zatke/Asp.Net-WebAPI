using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                //Add Roles
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered! Please Login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            //Find User
            var user = await userManager.FindByEmailAsync(loginRequestDTO.UserName);
            if(user != null)
            {
                //Check Password
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkPasswordResult)
                {
                    //getRoles
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //CreateToken
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                        var loginResponse = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(loginResponse);
                    }
                    
                }
            }
            return BadRequest("Username or password is Incorrect");
        }
    }
}
