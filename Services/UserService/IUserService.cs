using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_mk1.Models;

namespace API_mk1.Services.UserService
{
    public interface IUserService
    {
        Task<UserModel> GetUserByIdAsync(string id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(UserModel userModel); // might want to change return type later
        Task<UserModel> DeleteUserByIdAsync(string UserID);
        Task<UserModel> UpdateUserNameByUserId(string UserID, string newUserName);
    }
}
