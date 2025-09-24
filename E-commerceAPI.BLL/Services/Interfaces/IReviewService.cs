using E_commerceAPI.DAL.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool>AddReviewAsync(ReviewRequest reviewRequest, string userId);
    }
}
