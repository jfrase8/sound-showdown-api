using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundShowdownAPI.Models;
using System.Security.Claims;

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
            user.Id = this.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            user.Name = this.User.Identity.Name;
            user.Emails = ParseEmails(this.User.Claims.First(claim => claim.Type == "emails").Value);
            user.GivenName = this.User.Claims.First(claim => claim.Type == ClaimTypes.GivenName).Value;
            user.FamilyName = this.User.Claims.First(claim => claim.Type == ClaimTypes.Surname).Value;
            return user;
        }

        private List<String> ParseEmails(string emails)
        {
            var result = new List<String>();
            var emailArray = emails.Split(",");
            for (int i = 0; i < emailArray.Length; i++) 
            {
                emailArray[i] = emailArray[i].Trim();
                if (emailArray[i] != "") result.Add(emailArray[i]);
            }
            return result;
        }
    }
}
