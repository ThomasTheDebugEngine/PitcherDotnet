using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_mk1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace API_mk1.Services.AuthService
{
    public interface IAuthService
    {
        //public string GenerateJWT(UserAuthModel userAuthModel);

        Task<IdentityResult> Signup(IdentityUser identityUser, string passwordText);

        Task LogoutAsync();

        Task<SignInResult> Login(string password, string email);

        Task<IdentityUser> GetIdentUserByEmail(string email);
    }
}
