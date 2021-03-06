using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Dtos;
using API_mk1.Models;
using API_mk1.Services.AuthService;
using API_mk1.Services.ProjectService;
using API_mk1.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_mk1.Controllers
{
    [Route("api")]
    [ApiController]
    //[EnableCors("AllowAnyOrigin")]
    [AllowAnonymous]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public ProjectController(
            IMapper mapper,
            IProjectService projectService,
            IUserService userService,
            IAuthService authService)
        {
            _projectService = projectService;
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        #region GetProjectRoutes
        // GET: api/users/{id:string}/projects
        [HttpGet("users/{UserID}/projects", Name="GetUserAllProjects")]
        public async Task<ActionResult<List<ProjectGetDto>>> GetUserAllProjects(string UserID)
        {
            IdentityUser identUser = await _authService.GetIdentUserById(UserID);
            //HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if(identUser != null)
            {
                IList<ProjectModel> projectModel = await _projectService.GetAllUserProjectsByUserId(identUser.Id);

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Ok(_mapper.Map<IList<ProjectGetDto>>(projectModel));
            }
            else
            {
                //Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return NotFound();
            }
        }

        // GET: api/projects/{id:string}
        [HttpGet("projects/{ProjectID}", Name="GetSingleProjectById")]
        public async Task<ActionResult<ProjectGetDto>> GetSingleProjectById(string ProjectID)
        {
            ProjectModel projectModel = await _projectService.GetSingleProjectByProjectId(ProjectID);

            if(projectModel != null)
            {

                return Ok(_mapper.Map<ProjectGetDto>(projectModel));
            }
            else
            {
                return NotFound();
            }
        }

        //GET: api/projects/{projectId:string}/like/{userId:string}
        [HttpGet("projects/{ProjectID}/like/{UserID}", Name="LikeOrUnlikeProject")]
        public async Task<ActionResult<ProjectGetDto>> LikeOrUnlikeProject(string UserID, string ProjectID)
        {
            IdentityUser identUser = await _authService.GetIdentUserById(UserID);

            if(identUser != null)
            {
                ProjectModel projectModel = await _projectService.ToggleProjectLikeByUserId(ProjectID, UserID);

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Ok(_mapper.Map<ProjectGetDto>(projectModel));
            }
            return NotFound();
        }

        //GET: api/projects/{projectId:string}/star/{userId:string}
        [HttpGet("projects/{ProjectID}/star/{UserID}", Name="StarOrUnstarProject")]
        public async Task<ActionResult<bool>> StarOrUnstarProject(string UserID, string ProjectID)
        {
            IdentityUser identUser = await _authService.GetIdentUserById(UserID);

            if(identUser != null)
            {
                await _projectService.ToggleProjectStarredByUserId(ProjectID, UserID);

                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //TODO return new all starred projects
                return RedirectToAction("GetAllStarredProjectsByUserId", new { UserID });
            }
            return NotFound();
        }

        //GET: api/projects/{userID}/starred
        [HttpGet("projects/{UserID}/starred", Name="GetAllStarredProjectsByUserId")]
        public async Task<ActionResult<IList<ProjectGetDto>>> GetAllStarredProjectsByUserId(string UserID)
        {
            IdentityUser identUser = await _authService.GetIdentUserById(UserID);

            if(identUser != null)
            {
                IList<ProjectModel> starredProjects = await _projectService.GetAllStarredProjectsByUserId(UserID);
                return Ok(_mapper.Map<IList<ProjectGetDto>>(starredProjects));
            }
            return null;
        }

        #endregion

        // POST: api/projects
        [HttpPost("projects")]
        public async Task<ActionResult<ProjectGetDto>> CreateProject(ProjectPostDto projectPostDto)
        {
            if(ModelState.IsValid)
            {
                ProjectModel projectModel = _mapper.Map<ProjectModel>(projectPostDto);
                await _projectService.AddProject(projectModel);

                ProjectGetDto projectGetDto = _mapper.Map<ProjectGetDto>(projectModel);
                return CreatedAtAction("GetSingleProjectById", new { ProjectID = projectGetDto.ProjectId }, projectGetDto);

            }

            IEnumerable<string> errorList = ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage));

            return Ok(errorList);
        }

        // DELETE: api/projects/{id:string}
        [HttpDelete("projects/{projectID}", Name="DeleteProjectById")] //TODO need a way to verify user via auth (do later)
        public async Task<ActionResult> DeleteProjectById(string projectID)
        {
            ProjectModel projectModel = await _projectService.DeleteProjectById(projectID);
            ProjectGetDto projectGetDto = _mapper.Map<ProjectGetDto>(projectModel);

            return Ok(projectGetDto);
        }

        // PUT: api/projects/{id:string}
        [HttpPut("projects/{projectID}", Name="UpdateProjectById")]
        public async Task<ActionResult<ProjectGetDto>> UpdateProjectById(string projectID, ProjectPostDto projectPostDto)
        {
            ProjectModel updatedProjectModel = await _projectService.UpdateProjectByProjectIdAsync(projectID, projectPostDto);
            ProjectGetDto projectGetDto = _mapper.Map<ProjectGetDto>(updatedProjectModel);
            
            return CreatedAtAction("GetSingleProjectById", new { projectID = projectGetDto.ProjectId }, projectGetDto);
        }
    }
}
