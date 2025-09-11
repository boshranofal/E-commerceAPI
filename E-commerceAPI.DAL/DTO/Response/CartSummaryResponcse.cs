using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.DTO.Response
{
    public class CartSummaryResponcse
    {
        public List<CartResponse> Items { get; set; } = new List<CartResponse>();

        public decimal TotalCart => Items.Sum(i => i.TotalPrice);
    }
}
