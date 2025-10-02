using System.Text.Json.Serialization;

namespace productapi.model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaxCategory
{
    P,
    T,
    K,
}