using E_commerceAPI.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Intefaces
{
    public interface IProductRepository : IGenericReposetories<Product>
    {
        public Task DecreaseQuantity(List<(int productId, int quantity)> items);

        List<Product> GetAllproductWithImage();
    }
}
