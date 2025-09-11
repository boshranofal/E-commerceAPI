using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Classes;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.DTO.Request;
using KAStore.DAL.DTO.Responce;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class ProductServices : GenericServices<ProductRequest, ProductResponce, Product>,IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductServices(IProductRepository productRepository,IFileService fileService) : base(productRepository)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateFile(ProductRequest request)
        {
           var entity=request.Adapt<Product>();
            entity.Created= DateTime.UtcNow;
            if (request.ImageMain != null)
            {
               var imagePath= await _fileService.UplodeAsync(request.ImageMain);
                entity.ImageMain = imagePath;
            }
            return  _productRepository.Add(entity);
        }
    }
}
