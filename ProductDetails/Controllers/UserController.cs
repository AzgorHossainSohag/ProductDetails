using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductDetails.Models;
using ProductDetails.Token;

namespace ProductDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private UserManager<Users> _users;
        private SignInManager<Users> _signInManager;

        public UserController(UserManager<Users> users, SignInManager<Users> signInManager)
        {
            _users = users;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(string email, string password)
        {
            var user = new Users
            {
                Email = email,
                UserName = email
            };

            await _users.CreateAsync(user, password);
            await _signInManager.PasswordSignInAsync(email, password, true, false);
            return Ok($"user {email} successfully created");

        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> login(string email, string password)
        {

            await _signInManager.PasswordSignInAsync(email, password, true, false);
            return Ok(TokenGn.tokengen(email));

        }
    }
}
