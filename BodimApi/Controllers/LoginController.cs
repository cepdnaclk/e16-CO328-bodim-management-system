using BodimApi.Services;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace BodimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public class loginModel
        {
            [Required]
            [EmailAddress]
            public string email { get; set; }
            [Required]
            [MinLength(6)]
            public string password { get; set; }
        }

        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<string> Get(loginModel login)
        {
            var user = _userService.GetUser(login.email);

            if (user != null)
            {
                Boolean verified = BC.Verify(login.password, user.Password);
                if (verified) return user.Id;

            }
            return NotFound("Error: Input email or password is incorrect!");
        }

    }
}