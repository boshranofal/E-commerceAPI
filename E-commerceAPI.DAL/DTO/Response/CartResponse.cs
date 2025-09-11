using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.DTO.Response
{
    public class CartResponse
    {
        public int productId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Count;

    }
}
