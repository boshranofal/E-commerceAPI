using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface ICartService
    {
       Task< bool> AddToCartAsync(CartRequest cartRequest, string userId);
       Task< CartSummaryResponcse> CartSummaryResponcseAsync(string userId);
        Task<bool> ClearCartAsync(string userId);
    }
}
