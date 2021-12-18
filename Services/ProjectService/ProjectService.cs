using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using API_mk1.Security;
using API_mk1.Models;
using API_mk1.Services.UserService;

namespace API_mk1.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly PitcherContext _context;
        private readonly IUserService _userService;
        private readonly ISecurityUtils _secUtils;

        public ProjectService(PitcherContext context, IUserService userService, ISecurityUtils secUtils)
        {
            _userService = userService;
            _context = context;
            _secUtils = secUtils;
        }

        #region RetrieveMethods

        public async Task<IList<ProjectModel>> GetAllUserProjectsByUserId(string UserID)
        {
            return await Task.Run(() => _context.Projects.Where(p => p.OwnerId == UserID).ToList());
        }

        public Task<ProjectModel> GetSingleProjectByProjectId(string projectID)
        {
            return Task.Run(() => _context.Projects.FirstOrDefault(p => p.ProjectId == projectID));
        }

        public async Task<IList<ProjectModel>> GetPopularProjects() //TODO tie into likes later
        {
            IList<ProjectModel> topNResults = await Task.Run(() => _context.Projects.Where(p => p.likeNumber >= 0).ToList());
            return topNResults;
        }

        #endregion

        public async Task AddProject(ProjectModel projectModel) //TODO make a route for this and test
        {
            projectModel.ProjectId = await _secUtils.getGuidAsync();
            projectModel.CreatedAtUnix = await _secUtils.getUnixSecondsAsync();


            await _context.Projects.AddAsync(projectModel);
            await _context.SaveChangesAsync(); //WARN uncomment to actualy save to DB
        }

        public async Task<ProjectModel> DeleteProjectById(string ProjectID)
        {
            ProjectModel projectModel = await GetSingleProjectByProjectId(ProjectID);

            if(projectModel != null)
            {
                _context.Projects.Remove(projectModel);
                await _context.SaveChangesAsync(); //WARN uncomment to actually save
                return projectModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProjectModel> UpdateProjectByProjectIdAsync(string projectID, object newProjectModel)
        {
            ProjectModel DbProjectModel = await GetSingleProjectByProjectId(projectID);

            if(DbProjectModel != null)
            {
                //TODO might want to remove OwnerId modification into its own route
                ProjectModel DxProjectModel = (ProjectModel) _secUtils.AssignDifferential(newProjectModel, DbProjectModel);
                
                _context.Update(DxProjectModel);
                await _context.SaveChangesAsync(); //WARN uncomment to actually save
                return DxProjectModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<ProjectModel> ToggleProjectLikeByUserId(string projectID, string userID)
        {
            ProjectModel DbProjectModel = await GetSingleProjectByProjectId(projectID);

            if(DbProjectModel != null)
            {
                LikeModel likeProject = _context.LikeModel.FirstOrDefault(p => p.ProjectId == projectID && p.LikedBy == userID);

                if(likeProject != null)
                {
                    await Task.Run(() => _context.LikeModel.Remove(likeProject));

                    DbProjectModel.likeNumber -= 1;
                    return await UpdateProjectByProjectIdAsync(projectID, DbProjectModel);
                }
                else
                {
                    var record = new LikeModel
                    {
                        LikedBy = userID,
                        ProjectId = projectID
                    };

                    await _context.LikeModel.AddAsync(record);

                    DbProjectModel.likeNumber += 1;
                    return await UpdateProjectByProjectIdAsync(projectID, DbProjectModel);
                }
            }
            return null;
        }

        public async Task<bool> ToggleProjectStarredByUserId(string projectID, string userID)
        {
            ProjectModel DbProjectModel = await GetSingleProjectByProjectId(projectID);

            if(DbProjectModel != null)
            {
                StarModel starProject = _context.StarModel.FirstOrDefault(p => p.ProjectId == projectID && p.StarredBy == userID);

                if(starProject != null)
                {
                    await Task.Run(() => _context.Remove(starProject));
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var record = new StarModel
                    {
                        ProjectId = projectID,
                        StarredBy = userID
                    };

                    await _context.AddAsync(record);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<IList<ProjectModel>> GetAllStarredProjectsByUserId(string userID)
        {
            IQueryable<string> projectList = await Task.Run(() => _context.StarModel.Where(p => p.StarredBy == userID).Select(p => p.ProjectId));

            if(projectList.Any())
            {
                return await Task.Run(() => _context.Projects.Join(projectList, proj => proj.ProjectId, id => id, (proj, id) => proj).ToList());
            }
            return null;
        }
    }
}
