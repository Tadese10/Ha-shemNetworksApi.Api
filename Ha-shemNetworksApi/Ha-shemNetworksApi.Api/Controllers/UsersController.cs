using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ha_shemNetworksApi.Api.ServiceInterfaces;
using Ha_shemNetworksApiCommon.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ha_shemNetworksApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterNewUser([FromBody]UserRegistrationDO userParam)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.RegisterNewUser(new User
                {
                    FirstName = userParam.FirstName,
                    LastName = userParam.LastName,
                    Password = userParam.Password,
                    Role = Role.User,
                    Username = userParam.Username
                });

                if (!user.Item1)
                    return BadRequest(new { message = user.Item2 });

                return Ok(new { data = user.Item3,message =  user.Item2 });
            }
            else
            {
                return BadRequest(new { message = "Invalid input(s)" });
            }

        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAllUsers()//Get all register users
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}