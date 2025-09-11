using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.DTO.Request;
using KAStore.DAL.DTO.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class CategoryServices: GenericServices<CategoryRequest,CategoryResponce,Category>,ICategoryServices
       
    {
        public CategoryServices(ICategoyRepository categoyRepository): base(categoyRepository){ }
    }
}
