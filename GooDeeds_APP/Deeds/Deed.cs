using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GooDeeds_APP.Deeds
{
    public class Deed
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Experience { get; set; }
        
        [JsonPropertyName("deed_generator")]
        public int DeedGenerator { get; set; }
    }
}
