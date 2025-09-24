using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task <UserDto> GetByIdAsync(string userId);
        Task<bool> BlockUserAsync(string id, int days);
         Task<bool> UnBlockUserAsync(string id);
         Task<bool> IsBlockUserAsync(string id);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);
    }
}
