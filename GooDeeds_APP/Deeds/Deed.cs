using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GooDeeds_APP.Deeds
{
    /// <summary>
    /// This class is, like many other classes, mostly just a mapping-class for the JSON-String returned by the API.
    /// It holds every (needed) property of the JSON-String.
    /// </summary>
    public class Deed
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Experience { get; set; }
        
        [JsonProperty(PropertyName = "deed_generator")]
        public int DeedGenerator { get; set; }
    }
}
