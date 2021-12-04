using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Dtos;
using API_mk1.Models;
using API_mk1.Services.ProjectService;
using API_mk1.Services.UserService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API_mk1.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProjectController(IMapper mapper, IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
            _mapper = mapper;
        }

        #region GetProjectRoutes
        // GET: api/users/{id:string}/projects
        [HttpGet("users/{UserID}/projects", Name="GetUserAllProjects")]
        public async Task<ActionResult<List<ProjectGetDto>>> GetUserAllProjects(string UserID)
        {
            UserModel userModel = await _userService.GetUserByIdAsync(UserID);

            if(userModel != null)
            {
                IList<ProjectModel> projectModel = await _projectService.GetAllUserProjectsByUserId(userModel.UserId);
                return Ok(_mapper.Map<IList<ProjectGetDto>>(projectModel));
            }
            else
            {
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

        #endregion

        // POST: api/projects
        [HttpPost("projects")]
        public async Task<ActionResult<ProjectGetDto>> CreateProject(ProjectPostDto projectPostDto)
        {
            if(projectPostDto != null)
            {
                ProjectModel projectModel = _mapper.Map<ProjectModel>(projectPostDto);
                await _projectService.AddProject(projectModel);

                ProjectGetDto projectGetDto = _mapper.Map<ProjectGetDto>(projectModel);
                return CreatedAtAction("GetSingleProjectById", new { ProjectID = projectGetDto.ProjectId }, projectGetDto);
            }
            else
            {
                return Ok("post request was null"); //change from Ok() later
            }
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
