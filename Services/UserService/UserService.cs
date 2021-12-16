using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using API_mk1.Models;
using API_mk1.Security;
using Microsoft.EntityFrameworkCore;

namespace API_mk1.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly PitcherContext _context;
        private readonly ISecurityUtils _secUtils;

        public UserService(PitcherContext context, ISecurityUtils secUtils)
        {
            _context = context;
            _secUtils = secUtils;
        }

        public Task<UserModel> GetUserByIdAsync(string id) // ALL THIS IS DEPRECATED, REFACTOR FOR USAGE FROM AUTH
        {
            return Task.Run(() => _context.PitcherUsers.FirstOrDefault(p => p.UserId == id));
        }

        public Task<List<UserModel>> GetAllUsersAsync()
        {
            return Task.Run(() => _context.PitcherUsers.ToList());
        }

        public async Task<bool> CreateUserAsync(UserModel userModel)
        {
            if(userModel != null)
            {
                string userId = await _secUtils.getGuidAsync(); //this is totally unique now, allows for same name creation
                string passwordHash = _secUtils.GetSHA256(userModel.Password);

                if(await GetUserByIdAsync(userId) == null)
                {
                    userModel.Password = passwordHash;
                    userModel.UserId = userId;
                    await _context.PitcherUsers.AddAsync(userModel);
                    await _context.SaveChangesAsync(); //WARN uncomment to actually save to DB
                    return false;
                }
                else
                {
                    //The username is already taken, cannot create
                    //TODO move this decision up to the controller
                    return true;
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(userModel));
            }
        }

        public async Task<UserModel> DeleteUserByIdAsync(string UserID)
        {
            UserModel userModel = await GetUserByIdAsync(UserID);

            if(userModel != null)
            {
                _context.Remove(userModel);
                await _context.SaveChangesAsync(); //WARN uncomment to actually save
                return userModel;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserModel> UpdateUserNameByUserId(string UserID, string newUserName)
        {
            UserModel DbUserModel = await GetUserByIdAsync(UserID);

            if(DbUserModel != null)
            {
                DbUserModel.UserName = newUserName;
                _context.Update(DbUserModel);
                await _context.SaveChangesAsync(); //WARN uncomment to actually save
                return DbUserModel;
            }
            else
            {
                return null;
            }

        }
    }
}
