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
    public interface ICheckoutService
    {
        Task<CheckoutResponse> ProccessPaymentAsync(CheckoutRequest request, string UserId, HttpRequest httpRequest);
        Task<bool>HendelPaymentSuccessAsync(int orderId);
    
    }

}
