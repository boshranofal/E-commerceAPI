using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_commerceAPI.DAL.DTO.Response
{
    public class ProductResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public string ImageMain { get; set; }
        public string ImageMainUrl { get; set; }
        public List<string>SubImageUrls { get; set; } = new List<string>();
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}
