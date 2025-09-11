using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Model
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public double Rate { get; set; }
       public string ImageMain { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }

        public  Category Category { get; set; }
        public Brand? Brand { get; set; }
    }
}
