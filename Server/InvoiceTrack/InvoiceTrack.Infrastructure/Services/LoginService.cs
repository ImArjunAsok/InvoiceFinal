using InvoiceTrack.Core.Application.Interfaces;
using InvoiceTrack.Core.Domain.Dto;
using InvoiceTrack.Core.Domain.Models;
using InvoiceTrack.Core.Domain.Type;
using InvoiceTrack.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace InvoiceTrack.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly InvoiceTrackDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginService(InvoiceTrackDbContext db,
                            SignInManager<ApplicationUser> signInManager,
                            IConfiguration configuration,
                            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<ServiceResponse<string>> AdminLoginAsync(LoginDto dto)
        {
            var response = new ServiceResponse<string>();
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                response.AddError("", "Email entered not found.");
                return response;
            }
            var signin = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, true);
            if (signin.Succeeded)
            {
                response.Result = GenerateToken(user);
                return response;
            }
            response.AddError("", "Unable to login, recheck your email and password.");
            return response;
        }

        public string GenerateToken(ApplicationUser user)
        {
            string key = _configuration["Jwt:Key"];
            string issuer = _configuration["Jwt:Issuer"];
            string audience = _configuration["Jwt:Audience"];
            var role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().First();
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(signingKey, "HS256");
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, role),
                new Claim("Name", $"{user.FirstName} {user.LastName}"),
                new Claim("Email", user.Email),
                new Claim("Role", role),
                new Claim("UserId", user.Id),
            };
            var token = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                audience: audience,
                expires: DateTime.UtcNow + TimeSpan.FromDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResponse<string>> ExternalAuthenticationAsync(string token, string provider)
        {
            var response = new ServiceResponse<string>();
            var newToken = new JwtSecurityToken(token);
            var claims = newToken.Claims;
            string email = claims.First(c => c.Type == "preferred_username").Value;
            string oid = claims.First(c => c.Type == "oid").Value;
            string fullName = claims.First(c => c.Type == "name").Value;
            string[] nameParts = fullName.Split(' ');
            string firstName = nameParts[0];
            string[] lastNameParts = nameParts.Skip(1).ToArray();
            string lastName = string.Join(" ", lastNameParts);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                response.AddError("", "Your authorization failed.Please try refreshing the page and fill in the correct credentials.");
                return response;
            }
            var demo = await _db.UserLogins.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (demo == null)
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                await _db.SaveChangesAsync();
                var loginInfo = new UserLoginInfo(provider, oid, provider);
                var signin = await _userManager.AddLoginAsync(user, loginInfo);
                if (signin.Succeeded)
                {
                    response.Result = GenerateToken(user);
                    return response;
                }
                response.AddError("", "Your authorization failed. Please try refreshing the page and fill in the correct credentials.");
                return response;
            }
            else
            {
                response.Result = GenerateToken(user);
                return response;
            }
        }
    }
}