using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
          await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrder(int orderId)
        {
           return await _context.Orders.Include(o=>o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
