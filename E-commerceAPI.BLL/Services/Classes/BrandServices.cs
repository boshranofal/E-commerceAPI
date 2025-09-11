using E_commerceAPI.BLL.Services.Interfaces;
using E_commerceAPI.DAL.Model;
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
    public class BrandServices : GenericServices<BrandRequest, BrandResponce, Brand>, IBrandServices
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;

        public BrandServices(IBrandRepository brandRepository,IFileService fileService) : base(brandRepository)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
        }

        public async Task<int>CreateFile(BrandRequest request)
        {
            var entity = request.Adapt<Brand>();
            entity.Created = DateTime.UtcNow;
            if (entity.ImageMain != null)
            {
                var imagePath=await _fileService.UplodeAsync(request.ImageMain);
                entity.ImageMain = imagePath;
            }
            return _brandRepository.Add(entity);
        }

    }
}
