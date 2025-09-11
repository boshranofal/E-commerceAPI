using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KAStore.DAL.DTO.Responce
{
    public class BrandResponce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string ImageMain { get; set; }
        public string ImageMainUrl => $"https://localhost:7221/Image/{ImageMain}";
    }
}
