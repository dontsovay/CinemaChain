using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CinemaChain.API.AuthModel;
using CinemaChain.API.Helpers;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.ViewModels;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaChain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(UserManager<AppUser> userManager, IMapper mapper, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            // if (_userService.getUsers()==null)
            // { 
            //_userService.CreateUser( Username = "Avengers", Password = 1234, Role = Role.OWNER, FIO = "", Phone = "", Adress = ""  });

            //    dbc.SaveChanges();
        }
        // GET: api/Users
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

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int? id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id == null || id <= 0)
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(()=> _userService.UpdateUser(users));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [EnableCors]
        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] RegistrationVM user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //IdentityResult resul = await _roleManager.CreateAsync(new IdentityRole("client"));
            //resul = await _roleManager.CreateAsync(new IdentityRole("owner"));
            //resul = await _roleManager.CreateAsync(new IdentityRole("admin"));
            AppUser userIdentity = new AppUser();
            userIdentity.UserName = user.Username;
            var result = await _userManager.CreateAsync(userIdentity, user.Password);
            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            //var isSucsess = _UserService.addUser(userIdentity);
            var x = await _userManager.AddToRoleAsync(userIdentity, "client");
            await Task.Run(()=> _userService.CreateUser(user, userIdentity.Id));

            return Ok(user);
            //return CreatedAtAction("GetUsers", new { id = users.Username }, users);
            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {
            if (username == "")
            {
                return BadRequest();
            }

            try
            {
                await Task.Run(()=> _userService.DeleteUser(username));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool UsersExists(string username)
        {
            return _userService.GetById(username) != null;
        }
    }
}
