using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Core.Features.User.Services.Interfaces;
using Identity.Core.Features.User.Models;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await userService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            return Ok(await userService.SaveUser(userModel));
        }
    }
}
