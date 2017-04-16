using Newtonsoft.Json;
using ProIdeas.Domain.Repositories;

namespace ProIdeas.Domain.Entities
{
    public abstract class BaseEntity : IEntity
    {

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }


        
    }    
}
