using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrder(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<List<Order>> GetByStatus(StatusOrderEnum statusOrder);
        Task<List<Order>> GetOrderByUser(string userId);
        Task<bool> ChangeStatusAsync(string userId, StatusOrderEnum newStatus);
    }
}
