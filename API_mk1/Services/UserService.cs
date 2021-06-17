using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using API_mk1.Models.User;
using API_mk1.Security;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Services
{
    public class UserService: IUserService
    {
        private readonly PitcherContext _context;

        public UserService(PitcherContext context)
        {
            _context = context;
        }

        public Task<UserModel> GetUserByIdAsync(string id) // TODO handle 404 (on upper levels)
        {
            return Task.Run(() => (_context.Users.FirstOrDefault(p => p.UserId == id)));
        }

        public Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return Task.Run(() => (IEnumerable<UserModel>)_context.Users.ToList());
        }

        public Task<bool> SaveChangesAsync()
        {
            return Task.Run(() => _context.SaveChanges() >= 0);
        }

        public async Task<bool> CreateUserAsync(UserModel userModel)
        {
            if(userModel != null)
            {
                string userId = await Task.Run(() => StringHash.GetSHA2(userModel.UserName));

                if(await GetUserByIdAsync(userId) == null)
                {
                    userModel.UserId = userId;
                    _context.Users.Add(userModel);
                    //await SaveChangesAsync(); //WARN uncomment to actualy save to DB
                    return true;
                }
                else
                {
                    //The username is already taken, cannot create
                    return false;
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(userModel));
            }
        }

        //----------------------------------------------------------------------------------------
    }

    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(string id);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<bool> SaveChangesAsync();
        Task<bool> CreateUserAsync(UserModel userModel); // might want to change return type later
    }
}
