using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Model
{
    //Compisite Key
    [PrimaryKey(nameof(productId), nameof(userId))]
    public class Cart
    {
        public int productId { get; set; }
        public Product Product { get; set; }
        public string userId { get; set; }  
        public ApplicationUser User { get; set; }
        public int Count { get; set; }

    }
}
