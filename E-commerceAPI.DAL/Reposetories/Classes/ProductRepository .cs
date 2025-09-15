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
    public class ProductRepository :GenericRepsitory<Product> , IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DecreaseQuantity(List<(int productId, int quantity)>items)
        {
            var productItems= items.Select(i => i.productId).ToList();
            var products=await _context.Products.Where(p => productItems.Contains(p.Id)).ToListAsync();

            foreach (var item in items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.productId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.productId} not found.");
                }
                if (product.Quantity < item.quantity)
                {
                    throw new Exception($"Not enough stock available for product ID {item.productId}.");
                }
                product.Quantity -= item.quantity;
                await _context.SaveChangesAsync();
            }
           
            
        }
    }
}
