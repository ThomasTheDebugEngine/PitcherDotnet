using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using API_mk1.Security;
using API_mk1.Models.Project;
using API_mk1.Models.User;


namespace API_mk1.Services
{
    public class ProjectService : IProjectService
    {
        private readonly PitcherContext _context;

        public ProjectService(PitcherContext context)
        {
            _context = context;
        }

        public Task<bool> SaveChangesAsync()
        {
            return Task.Run(() => _context.SaveChanges() >= 0);
        }   

        public async void AddProject(string UserName, string title, string body)
        {
            ProjectModel projectModel = new ProjectModel();
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if(UserName.Length == 0 || title.Length == 0 || body.Length == 0)
            {
                Console.WriteLine("cannot enter empty body or title"); //TODO need to make it an HTTP return
            }
            else
            {
                projectModel.OwnerId = await Task.Run(() => StringHash.GetSHA2(UserName));
                projectModel.Title = title;
                projectModel.Body = body;
                projectModel.CreatedAtUnix = timestamp;
                projectModel.ProjectId = await Task.Run(() => StringHash.GetSHA2(title + body + timestamp.ToString()));

                _context.Projects.Add(projectModel);

                //await SaveChangesAsync(); //WARN uncomment to actualy save to DB
            }
        }

        public async void GetAllUserProjects(string UID)
        {
            UserModel usermodel = await Task.Run(() => _context.Users.FirstOrDefault(p => p.UserId == UID));

            if(usermodel == default(UserModel))
            {
                Console.WriteLine("user was not found"); //TODO need to make it an HTTP return
            }
            else
            {
                _context.Entry(usermodel).Collection(usermodel => usermodel.ProjectModel).Load(); //TODO need to test this (link first)
            }
            
        }
    }

    interface IProjectService
    {

    }
}
