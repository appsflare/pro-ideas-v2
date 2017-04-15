
namespace ProIdeas.Serializers.Contracts
{
    public interface IJsonSerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
