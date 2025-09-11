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
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string ImageMain { get; set; }
        public string ImageMainUrl=> $"https://localhost:7221/Image/{ImageMain}";
    }
}
