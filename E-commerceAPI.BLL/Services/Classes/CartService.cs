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
        public bool AddToCart(CartRequest request, string userId)
        {
            var newCart = new Cart
            {
                 productId= request.productId,
                 userId= userId,
                 Count=1
                };
            return _cartRepository.Add(newCart)>0;
            }

        public CartSummaryResponcse CartSummaryResponcse(string userId)
        {
            var cartItems = _cartRepository.GetUserCart(userId);
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
    }
    }

