using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Intefaces
{
    public interface ICartRepository
    {
        int Add(Cart cart);
        List<Cart> GetUserCart(string userId);
    }
}
