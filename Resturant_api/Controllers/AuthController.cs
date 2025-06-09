using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant_api.Model.DTO;
using Resturant_api.Repository;

namespace Resturant_api.Controllers
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

        //POST: api/auth/Register
        [HttpPost]
        [Route("Register")]
       public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
       {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

             var identityResult =  await userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult =  await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was registered.");
                    }
                }
            }
            return BadRequest("Something went wrong");
       }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
            
            if (user != null)
            { 
              var checkPasswordResult  = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //Get Roles
                    var Roles = await userManager.GetRolesAsync(user);
                    if (Roles != null)
                    {
                      var JwtToken = tokenRepository.CreateJWTToken(user, Roles.ToList());
                        var response = new JwtResponse
                        {
                            JwtToken = JwtToken
                        };
                      return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password Incorrect");
        }
    }
}
