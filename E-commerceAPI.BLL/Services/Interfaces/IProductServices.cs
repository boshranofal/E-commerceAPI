using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using KAStore.DAL.DTO.Request;
using KAStore.DAL.DTO.Responce;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IProductServices:IGenericServices<ProductRequest,ProductResponce,Product>
    {

       Task<int>CreateFile(ProductRequest request);
        Task<List<ProductResponce>> GetAllProduct(HttpRequest request, bool onlyActive = false);
    }
}
