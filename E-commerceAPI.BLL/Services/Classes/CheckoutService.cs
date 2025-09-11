using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;

using E_commerceAPI.DAL.Reposetories.Intefaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Stripe;
using Stripe.Checkout;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailSender _emailSender;

        public CheckoutService(ICartRepository cartRepository,IOrderRepository orderRepository,IEmailSender emailSender)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _emailSender = emailSender;
        }

        public async Task<bool> HendelPaymentSuccessAsync(int orderId)
        {
            var order = await _orderRepository.GetUserByOrder(orderId);
            var subject = "";
            var body = "";
            if (order.PaymentMethod == StatusPaymentMethodEnum.Visa)
            {
                subject = "Payment Success";
                body = $"<h1>Thank you for your Payment</h1>+" +
                    $" <p>your payment for order{orderId}</p>"+
                      $"<p>total amount:{order.TotalAmount}</p>";

               
            }
            if (order.PaymentMethod == StatusPaymentMethodEnum.Cach)
            {
                subject = "Order placed succesfully";
                body = $"<h1>Thank you for your order</h1>+" +
                    $" <p>your payment for order{orderId}</p>" +
                      $"<p>total amount:{order.TotalAmount}</p>";
            }

            await _emailSender.SendEmailAsync(order.User.Email, subject, body);
            return true;
        }

        public async Task<CheckoutResponse> ProccessPaymentAsync(CheckoutRequest request, string UserId, HttpRequest httpRequest)
        {
            var cartItems =  _cartRepository.GetUserCart(UserId);
            if (!cartItems.Any())
            {
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "Cart is empty",

                };
            }
            E_commerceAPI.DAL.Model.Order order = new E_commerceAPI.DAL.Model.Order
            {
                UserId = UserId,
                PaymentMethod=StatusPaymentMethodEnum.Cach,
                TotalAmount= cartItems.Sum(i => i.Product.Price * i.Count),
            };
            await _orderRepository.AddAsync(order);
            if (request.PaymentMethod == StatusPaymentMethodEnum.Cach)
                {
                    return new CheckoutResponse
                    {
                        Success = true,
                        Message ="Cach",

                    };
                }
                if (request.PaymentMethod == StatusPaymentMethodEnum.Visa)
                {
                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card" },
                        LineItems = new List<SessionLineItemOptions>
                        { 
               
                        },
                        Mode = "payment",
                        SuccessUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/Customer/CheckOuts/Success/{order.Id}",
                        CancelUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/checkout/cancel",
                    };
                    foreach (var item in cartItems)
                    {
                        options.LineItems.Add(

                             new SessionLineItemOptions
                             {
                                 PriceData = new SessionLineItemPriceDataOptions
                                 {
                                     Currency = "USD",
                                     ProductData = new SessionLineItemPriceDataProductDataOptions
                                     {
                                         Name = item.Product.Name,
                                         Description = item.Product.Description
                                     },
                                     UnitAmount = (long)item.Product.Price,
                                 },
                                 Quantity = item.Count,
                             }
                             );
                            
                    }
                    var service = new SessionService();
                    var session = service.Create(options);
                  order.PaymentId = session.Id;
                    return new CheckoutResponse
                    {
                        Success = true,
                        Message = "Payment processed successfully",
                        Url=session.Url,
                        PaymentId=session.Id

                    };
                }
                return new CheckoutResponse
                {
                    Success = false,
                    Message = "Invalid payment method",
                };
            }
        }
    }

