using Newtonsoft.Json;

namespace Findo.Framework.Http.Proxies
{
    // Defines an adapter interface for the serializers
    public interface ISerializerAdapter
    {
        string Serialize(object obj);
        T Deserialize<T>(string input);
    }

    // An implementation of ISerializerAdapter based on the JavaScriptSerializer
    public class SerializerAdapter : ISerializerAdapter
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }

    // The configuration class defines how the rest call is made.
    public class ClientConfiguration
    {
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public bool RequrieSession { get; set; }
        public ISerializerAdapter OutBoundSerializerAdapter { get; set; }
        public ISerializerAdapter InBoundSerializerAdapter { get; set; }
    }
}
