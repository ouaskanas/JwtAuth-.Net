using jwt.Models;
using jwt.Services;
using Microsoft.AspNetCore.Identity;
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

            if(await userManager.IsLockedOutAsync(user))
            {
                return Unauthorized(new { message = "User is locked out" });
            }
            // formating code : pascal, kebab, kamel
            var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.password, lockoutOnFailure :true);
            if (!result.Succeeded)
            {
                var count = 5 - await userManager.GetAccessFailedCountAsync(user);
                return Unauthorized(new { message = $"Invalid credentials , {count} times remains before the lock out !" });
            }
            
                var token = await jwtService.GenerateToken(user);

            //cookies option 
            var cookies = new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("jwt", token, cookies);

            return Ok("LOGGED IN");
        }

    }
}
