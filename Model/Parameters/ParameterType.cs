using System.Text.Json.Serialization;

namespace productapi.model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ParameterType
{
    [JsonStringEnumMemberName("object")]
    ObjectType,
    [JsonStringEnumMemberName("string")]
    StringType,
    [JsonStringEnumMemberName("boolean")]
    BooleanType,
    [JsonStringEnumMemberName("integer")]
    IntegerType,
}