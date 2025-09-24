using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Intefaces
{
    public interface IReviewRepository
    {
        Task<bool>HasUserReviewProduct(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);
    }
}
