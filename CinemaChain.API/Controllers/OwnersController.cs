using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CinemaChain.API.AuthModel;
using CinemaChain.API.Helpers;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using CinemaChain.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaChain.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public OwnersController(UserManager<AppUser> userManager, IMapper mapper, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        // GET: api/For_Owner
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _userService.getUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await Task.Run(() => _userService.GetById(username));

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }


        // POST: api/For_Owner

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationVM user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser userIdentity = new AppUser();
            userIdentity.UserName = user.Username;
            var result = await _userManager.CreateAsync(userIdentity, user.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            //var isSucsess = _UserService.addUser(userIdentity);
            var x = await _userManager.AddToRoleAsync(userIdentity, "admin");
            await Task.Run(() => _userService.CreateUserAdm(user, userIdentity.Id));

            return Ok(user);
        }

    }
}
