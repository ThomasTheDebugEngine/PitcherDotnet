using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_mk1.Models.User;
using API_mk1.Services;
using AutoMapper;
using API_mk1.Dtos;

namespace API_mk1.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserModelsController : ControllerBase
    {
        //private readonly IPitcherContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserModelsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        // GET: api/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<ReadDto>>> GetAllUsers()
        {
            var AllUsers = await _userService.GetAllUsersAsync();
            return Ok(_mapper.Map<IEnumerable<ReadDto>>(AllUsers));
        }


        // GET: api/users/{id}
        [HttpGet("users/{id}", Name= "GetUserById")]
        public async Task<ActionResult<ReadDto>> GetUserById(string id)
        {
            var userModel = await _userService.GetUserByIdAsync(id); //ERROR gives 400 or 404 need to figure out how to use this to advance

            if(userModel != null)
            {
                return Ok(_mapper.Map<ReadDto>(userModel));
            }
            else
            {
                return NotFound();
            }
            
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        // POST: api/signup
        [HttpPost("signup")]
        public async Task<ActionResult<ReadDto>> CreateUser(CreateDto createDto)
        {
            //TODO check input validity (EXP)

            var userModel = _mapper.Map<UserModel>(createDto);
            bool UserIsDuplicate = await _userService.CreateUserAsync(userModel);

            if(!UserIsDuplicate)
            {
                return Ok("user already taken");
            }
            else
            {
                var UserReadDto = _mapper.Map<ReadDto>(userModel);
                return CreatedAtAction("GetUserById", new { id = UserReadDto.UserId }, UserReadDto);
            }
        }


        //private bool UserModelExists(long id)
        //{
        //    return _userService.UserModel.Any(e => e.DbId == id);
        //}
    }
}
