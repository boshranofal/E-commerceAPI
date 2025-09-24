using E_commerceAPI.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UplodeAsync(IFormFile file)
        {
            if(file != null&& file.Length > 0)
            {
               var fileName= Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot" ,"Image", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
           throw new Exception("File is null or empty");
        }
        public async Task<List<string>> UplodeManyAsync(List<IFormFile> files)
        {
            var fileNames=new List<string>();

            foreach(var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileNames.Add(fileName);
                }
            }
            return fileNames;
        }

        }
    }

