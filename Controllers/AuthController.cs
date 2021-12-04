using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_mk1.Dtos;
using API_mk1.Models;
using API_mk1.Services.AuthService;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_mk1.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthController(
            IAuthService authService,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _authService = authService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // POST: api/signup
        [HttpPost("signup")]
        public async Task<IActionResult> UserLogin(UserSignInModel userSignInModel)
        {    
            if(ModelState.IsValid)
            {

                if(User.Identity.IsAuthenticated)
                {
                    return BadRequest("user is already logged in");
                }

                Microsoft.AspNetCore.Identity.SignInResult result = await _authService.Login(userSignInModel.Password, userSignInModel.Email);

                if(result.Succeeded)
                {
                    IdentityUser identityUser = await _authService.GetIdentUserByEmail(userSignInModel.Email);
                    return Ok(_mapper.Map<AuthOutgoingDto>(identityUser));
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            IEnumerable<string> errorList = ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage));

            return BadRequest(errorList);
        }

        // POST: api/register, this will replace the signup in UserController
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserSignupModel userSignupModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = userSignupModel.UserName,
                    Email = userSignupModel.Email
                };

                IdentityResult result = await _authService.Signup(identityUser, userSignupModel.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(identityUser, false);
                    return Ok(_mapper.Map<AuthOutgoingDto>(identityUser)); // TODO make created action
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            IEnumerable<string> errorList = ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage));

            return StatusCode(400, errorList);
        }

        // GET: api/login-status
        [HttpGet("login-status")]
        public async Task<ActionResult<bool>> CheckUserAuthStatus()
        {
            return await Task.Run(() => Ok(User != null && User.Identity.IsAuthenticated));
        }

        // POST: api/logout // this is an empty post to prevent CSRF escalation attacks
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if(User != null && User.Identity.IsAuthenticated)
            {
                await _authService.LogoutAsync();
                return Ok("logged out");
            }

            return BadRequest("user already logged out");
        }
    }
}
