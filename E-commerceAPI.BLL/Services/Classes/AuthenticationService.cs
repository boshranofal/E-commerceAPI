using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
                                       IConfiguration configuration,
                                      IEmailSender emailSender)
        {

            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task<UserResponce> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("invalid password or Email");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Email is not confirmed yet");
            }
            var isPassValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPassValid)
            {
                throw new Exception("Invalid Password");
            }
            return new UserResponce
            {
                Token = await CreateToken(user),
            };
        }
        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("user not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "email confirmed succeed";
            }
            return "email congirmation falid";
        }
        public async Task<UserResponce> RegisterAsync(RegisterRequest request,HttpRequest httpRequest)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var emailUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/identity/Account/ConfirmEmail?token={escapeToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "Confirm Email",
                    $"<h1>Welcome {user.FullName}</h1>" +
                    $"<a href='{emailUrl}'> confirm </a>");
                return new UserResponce
                {
                    Token = await CreateToken(user),
                };
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
        }

        private async Task<string> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTOptions")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(5),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ForgetPassword(ForgetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            user.CodeResetPassword = code;
            user.CodeResetPasswordExpiration = DateTime.Now.AddMinutes(10);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(user.Email, "Reset Password",
                $"<h1>Reset Password</h1>" +
                $"<p>Your reset code is: {code}</p>");

            return true;

        }

       
    }
}
