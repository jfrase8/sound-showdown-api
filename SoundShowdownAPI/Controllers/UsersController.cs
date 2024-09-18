using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundShowdownAPI.Models;

namespace SoundShowdownAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [Route("me")]
        [HttpGet]
        public User Me()
        {
            foreach (var item in HttpContext.Request.Headers)
            {
                Console.WriteLine(item);
            }

            var user = new User();
            user.Name = this.User.Identity.Name;
            user.Email = "unimplemented";
            return user;
        }
    }
}
