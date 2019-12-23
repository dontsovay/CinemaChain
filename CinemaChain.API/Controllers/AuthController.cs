using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaChain.API.Auth;
using CinemaChain.Infrastructure.Interfaces;
using CinemaChain.Models.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using CinemaChain.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CinemaChain.API.AuthModel;
using CinemaChain.Models.ViewModels;

namespace CinemaChain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/Auth
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJWTFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(IUserService userService, UserManager<AppUser> userManager, IJWTFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

    // GET: api/Login
    [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _userService.getUsers();
        }

        // GET: api/Login/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _userService.GetByIdNum(username);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }


        // POST: api/Login

        [EnableCors]
        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] CredetionalVM user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(user.UserName, user.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }
            AppUser userIdentity = await _userManager.FindByNameAsync(user.UserName);
            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, user.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            var rolesList = await _userManager.GetRolesAsync(userIdentity);
            var username = user.UserName;
            return new OkObjectResult('{' + "\"role\":" + JsonConvert.SerializeObject( rolesList.First()) + "," + "\"username\":" + JsonConvert.SerializeObject(username) + "," + jwt.TrimStart('{'));


            //var user = await Task.Run(()=>_userService.LOGIN(users.Username, users.Password));

            //if (user != null)
            //{
            //    var datas = Task.Run(() => _userService.WhatRoleOfUser(users.Username));
            //    return Ok(datas.Result);
            //}
            //else return NotFound();
            //if(user.Password.Equals(users.Password))
            //{
            //    return Ok(users.Role.ToString());
            //}

        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var a = _jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id);
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

    }
}
