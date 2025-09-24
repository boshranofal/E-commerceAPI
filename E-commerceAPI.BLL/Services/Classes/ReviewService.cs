using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class ReviewService : IReviewService
        
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId)
        {
            var hasOrder = await _orderRepository
                .UserHasApprovedOrderForProductAsync(userId, reviewRequest.ProductId);
                if (!hasOrder) return false;
                var areadyReviewed = await _reviewRepository.HasUserReviewProduct(userId, reviewRequest.ProductId);
            if(areadyReviewed) return false;

            var review = reviewRequest.Adapt<Review>();

            await _reviewRepository.AddReviewAsync(review,userId);
            return true;
        }
    }
}
