using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models;
using API_mk1.Services;
using AutoMapper;
using API_mk1.Services.UserService;
using API_mk1.Services.ProjectService;
using API_mk1.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace API_mk1.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowAnyOrigin")]
    public class UserController : ControllerBase
    {
        //private readonly IPitcherContext _context;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IProjectService projectService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _mapper = mapper;
        }

        #region GetRoutes


        // GET: api/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllUsers()
        {
            var AllUsers = await _userService.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserGetDto>>(AllUsers));
        }


        // GET: api/users/{id:string}
        [HttpGet("users/{UserID}", Name="GetUserById")] // user projects + user info (do after project getting is done)
        public async Task<ActionResult<UserGetDto>> GetUserById(string UserID)
        {
            UserModel userModel = await _userService.GetUserByIdAsync(UserID); //ERROR gives 400 or 404 need to figure out how to use this to advance

            if(userModel != null)
            {
                return Ok(_mapper.Map<UserGetDto>(userModel));
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: api/browse
        [HttpGet("browse", Name= "GetPopularProjects")]
        public async Task<ActionResult<List<ProjectGetDto>>> GetPopularProjects()
        {
            IList<ProjectModel> popularProjects = await _projectService.GetPopularProjects();

            if(popularProjects != null)
            {
                return Ok(_mapper.Map<IList<ProjectGetDto>>(popularProjects));
            }
            else
            {
                return NotFound();
            }
        }

        #endregion
        
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        //-------------------------------------------UPFATE THESE ROUTES TO IDENTITY LATER-------------------------------------
        // DELETE: api/users/{id:string}
        [HttpDelete("users/{UserID}", Name="DeleteUserById")]
        public async Task<ActionResult<UserGetDto>> DeleteUserById(string UserID)
        {
            UserModel userModel = await _userService.DeleteUserByIdAsync(UserID);
            UserGetDto userGetDto = _mapper.Map<UserGetDto>(userModel);

            return Ok(userGetDto);
        }

        // PUT: api/users/{id:string}
        [HttpPut("users/{UserID}", Name = "UpdadeUserNameById")]
        public async Task<ActionResult<UserGetDto>> UpdadeUserNameById(string UserID, UserPostDto userPostDto) //TODO need to verify a bunch of these Dtos
        {
            UserModel updatedUserModel = await _userService.UpdateUserNameByUserId(UserID, userPostDto.UserName);
            UserGetDto userGetDto = _mapper.Map<UserGetDto>(updatedUserModel);
            
            return CreatedAtAction("GetUserById", new { UserID = userGetDto.UserId }, userGetDto);
        }
    }
}
