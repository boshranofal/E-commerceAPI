using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Model
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
