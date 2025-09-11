using E_commerceAPI.DAL.Model;
using KAStore.DAL.DTO.Request;
using KAStore.DAL.DTO.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Interfaces
{
    public interface IBrandServices:IGenericServices<BrandRequest,BrandResponce,Brand>
    {
        Task<int> CreateFile(BrandRequest request);
      
    }
}
