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
        public int Add(Cart cart)
        {
            _context.Carts.Add(cart);
            return _context.SaveChanges();
        }

        public List<Cart> GetUserCart(string userId)
        {
            return _context.Carts.Include(c => c.Product).Where(c => c.userId == userId).ToList();
        }
    }
}
