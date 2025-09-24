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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> HasUserReviewProduct(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(u=>u.UserId == userId && u.ProductId == productId);
        }
        public async Task AddReviewAsync(Review request, string userId)
        {
             request.UserId = userId;
            request.ReviewDate = DateTime.Now;
            await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}
