using E_commerceAPI.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.DTO.Request
{
    public class ReviewRequest
    {
        public string Rate { get; set; }
        public string? Comment { get; set; }
        public int ProductId { get; set; }
        
    }
}
