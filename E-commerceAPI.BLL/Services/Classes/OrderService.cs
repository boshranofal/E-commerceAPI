using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class OrderService : IOrderService

    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order?> AddAsync(Order order)
        {
            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> ChangeStatusAsync(string userId, StatusOrderEnum newStatus)
        {
            return await _orderRepository.ChangeStatusAsync(userId, newStatus);
        }

        public async Task<List<Order>> GetByStatus(StatusOrderEnum statusOrder)
        {
            return await _orderRepository.GetByStatus(statusOrder);
        }

        public async Task<List<Order>> GetOrderByUser(string userId)
        {
           return await _orderRepository.GetOrderByUser(userId);
        }

        public async Task<Order?> GetUserByOrder(int orderId)
        {
            return await _orderRepository.GetUserByOrder(orderId);
        }
    }
}
