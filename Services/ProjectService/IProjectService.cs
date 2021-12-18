using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using API_mk1.Models;

namespace API_mk1.Services.ProjectService
{
    public interface IProjectService
    {
        Task AddProject(ProjectModel projectModel);

        Task<IList<ProjectModel>> GetAllUserProjectsByUserId(string UserID);

        Task<ProjectModel> GetSingleProjectByProjectId(string projectID);

        Task<IList<ProjectModel>> GetPopularProjects();

        Task<ProjectModel> DeleteProjectById(string ProjectID);
        
        Task<ProjectModel> UpdateProjectByProjectIdAsync(string projectID, object newProjectModel);

        Task<ProjectModel> ToggleProjectLikeByUserId(string projectID, string userID);

        Task<bool> ToggleProjectStarredByUserId(string projectID, string userID);

        Task<IList<ProjectModel>> GetAllStarredProjectsByUserId(string userId);
    }
}