using Newtonsoft.Json;
using ProIdeas.Serializers.Contracts;

namespace ProIdeas.Serializers
{
    public class JsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
