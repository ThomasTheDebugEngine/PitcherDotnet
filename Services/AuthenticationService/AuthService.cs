using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_mk1.Context.PitcherContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using API_mk1.Models;
using API_mk1.Security;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace API_mk1.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly PitcherContext context;
        private readonly IConfiguration config;
        private readonly ISecurityUtils secUtils;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthService(
            PitcherContext context,
            IConfiguration config,
            ISecurityUtils secUtils,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.config = config;
            this.secUtils = secUtils;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private string GenerateJWT(UserModel userModel)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:secret"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("UserID", userModel.UserId));

            var token = new JwtSecurityToken(
                config["jwt:issuer"],
                config["jwt:issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<SignInResult> Login(string password, string email) //TODO implement optional username or email later
        {
            IdentityUser identUser = null;

            if(email != null)
            {
                identUser = await userManager.FindByEmailAsync(email); // TODO make sure emails are unique on register

            }

            if(identUser != null)
            {
                return await signInManager.PasswordSignInAsync(identUser.UserName, password, false, false);
            }

            return SignInResult.Failed;
        }

        public async Task<IdentityResult> Signup(IdentityUser identityUser, string passwordText)
        {
            return await userManager.CreateAsync(identityUser, passwordText);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityUser> GetIdentUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> GetIdentUserById(string userID)
        {
            return await userManager.FindByIdAsync(userID);
        }
    }
}
