using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository) {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCartAsync(CartRequest request, string userId)
        {
            var newCart = new Cart
            {
                 productId= request.productId,
                 userId= userId,
                 Count=1
            };
            return await _cartRepository.AddAsync(newCart)>0;
            }

        public async Task<CartSummaryResponcse> CartSummaryResponcseAsync(string userId)
        {
            var cartItems =await  _cartRepository.GetUserCartAsync(userId);
            var response = new CartSummaryResponcse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    productId = ci.productId,
                    ProductName = ci.Product.Name,
                    Count =ci.Count ,
                    Price = ci.Product.Price,

                }).ToList()

            };
            return response;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await  _cartRepository.ClearCart(userId);
        }
    }
    }

