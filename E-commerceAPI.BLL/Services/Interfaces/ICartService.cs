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
        bool AddToCart(CartRequest cartRequest, string userId);
        CartSummaryResponcse CartSummaryResponcse(string userId);
    }
}
