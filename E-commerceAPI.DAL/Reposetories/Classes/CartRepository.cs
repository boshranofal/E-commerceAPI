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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<int> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var items=_context.Carts.Where(c => c.userId == userId).ToList();//هون بترجع اكتر من عنصر 
            _context.Carts.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> GetUserCartAsync(string userId)
        {
            return await _context.Carts.Include(c => c.Product).Where(c => c.userId == userId).ToListAsync();
        }
    }
}
