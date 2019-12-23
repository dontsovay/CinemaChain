using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CinemaChain.API.AuthModel;
using CinemaChain.API.Helpers;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Infrastructure.Services;
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
    public class PurchaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public PurchaseController(UserManager<AppUser> userManager, IMapper mapper, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        // GET: api/Purchase
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Purchase/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Purchase
        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> PostOwner([FromBody] RegistrationVM user)
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
            var x = await _userManager.AddToRoleAsync(userIdentity, "owner");
            await Task.Run(() => _userService.CreateUserOwn(user, userIdentity.Id));

            return Ok(user);
        }

        // PUT: api/Purchase/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
