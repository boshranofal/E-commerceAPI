using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.Reposetories.Classes
{
    public class BrandRepository :GenericRepsitory<Brand> ,IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
