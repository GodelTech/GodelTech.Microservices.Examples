using System.Text.Json.Serialization;

namespace Microservice.Domain
{
    public class Nickname
    {
        [JsonConstructor]
        public Nickname(
            string name,
            int? age)
        {
            Name = name;
            Age = age;
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("age")]
        public int? Age { get; }
    }
}
