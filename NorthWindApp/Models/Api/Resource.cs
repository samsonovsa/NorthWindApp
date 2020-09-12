using Newtonsoft.Json;

namespace NorthWindApp.Models.Api
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        public string Href { get; set; }  
    }
}
