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

        [Route("me")]
        [HttpGet]
        public User Me()
        {
            var user = new User();
            user.Name = this.User.Identity.Name;
            user.Email = "unimplemented";
            return user;
        }
    }
}
