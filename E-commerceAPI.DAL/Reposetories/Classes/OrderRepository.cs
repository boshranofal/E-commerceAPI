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
        public async Task<List<Order>>GetAllWithUserStatus(string userId)
        {
            return await _context.Orders.Where(o=>o.UserId==userId).ToListAsync();
        }
        public async Task<List<Order>> GetByStatus(StatusOrderEnum statusOrder)
        {
            return await _context.Orders.Where(o=>o.Status==statusOrder)
                .OrderByDescending(o=>o.OrderDate).ToListAsync();
        }
        public async Task<List<Order>> GetOrderByUser(string userId)
        {
          return await _context.Orders.Include(o => o.User)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
        }
        public async Task<bool>ChangeStatusAsync(string userId,StatusOrderEnum newStatus)
        {
            var user = await _context.Orders.FindAsync(userId);
            if (user == null) return false;

            user.Status= newStatus;
            var result= await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                .AnyAsync(e => e.UserId == userId && e.Status == StatusOrderEnum.approved&&
                e.OrderItems.Any(i=>i.ProductId==productId));
        }
    }
}
