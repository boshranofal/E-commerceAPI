using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Intefaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrder(int orderId);
        Task<Order?>AddAsync(Order order);
    }
}
