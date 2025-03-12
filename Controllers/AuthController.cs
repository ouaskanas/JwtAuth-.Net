using jwt.Models;
using jwt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> signIn([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User
            {
                name = registerModel.name,
                Email = registerModel.email,
                UserName = registerModel.email 
            };
            var result = await userManager.CreateAsync(user, registerModel.password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "User Created Successfully!" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(loginModel.email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var token = await jwtService.GenerateToken(user);
            return Ok(new
            {
                token = token,
                user = new
                {
                    id = user.Id,
                    Email = user.Email,
                    name = user.name
                }
            });
        }

    }
}
