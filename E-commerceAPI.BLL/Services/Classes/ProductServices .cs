using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.DTO.Request;
using E_commerceAPI.DAL.DTO.Response;
using E_commerceAPI.DAL.Model;
using E_commerceAPI.DAL.Reposetories.Classes;
using E_commerceAPI.DAL.Reposetories.Intefaces;
using KAStore.DAL.DTO.Request;
using KAStore.DAL.DTO.Responce;
using Mapster;
using Microsoft.AspNetCore.Http;
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
            if (request.SubImage != null)
            {
                var subImagesPasths=await _fileService.UplodeManyAsync(request.SubImage);
                entity.SubImage=subImagesPasths.Select(img=>new ProductImage{ImageName=img}).ToList();
            }
            return  _productRepository.Add(entity);
        }

        public async Task<List<ProductResponce>> GetAllProduct(HttpRequest request, bool onlyActive = false, int pageSize=1,int pageNumber =1)
        {
            var products = _productRepository.GetAllproductWithImage();
            if (onlyActive)
            {
                products=products.Where(p => p.Status == Status.Active).ToList();
            }
            var pageProducts=products.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();

            return pageProducts.Select(p => new ProductResponce
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                ImageMain = $"{request.Scheme}://{request.Host}/Image/{p.ImageMain}",
                SubImageUrls = p.SubImage.Select(img => $"{request.Scheme}://{request.Host}/Image/{img.ImageName}").ToList(),
                Reviews=p.Reviews.Select(p=>new ReviewResponse
                {
                    Id=p.Id,
                    Comment=p.Comment,
                    Rate=p.Rate,
                    FullNmae=p.User.FullName,
                
                }).ToList(),
            }).ToList();
        }
        }
}
