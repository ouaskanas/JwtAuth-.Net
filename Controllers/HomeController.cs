using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers
{
    [Route("/api/result")]
    [ApiController]
    public class HomeController : Controller
    {
    
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return Ok( new string("Welcome to Jwt, its working ! "));
        }



    }
}
