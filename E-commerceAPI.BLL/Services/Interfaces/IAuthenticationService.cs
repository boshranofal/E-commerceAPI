using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task <UserResponce>LoginAsync (LoginRequest request);
        Task<UserResponce> RegisterAsync(RegisterRequest request,HttpRequest httpRequest);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgetPassword(ForgetPasswordRequest request);
    }
}
