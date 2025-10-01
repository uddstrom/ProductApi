using System.Text.Json.Serialization;

namespace productapi.model;

public abstract record SchemaBase
{
  [JsonPropertyName("$id")]
  public string Id { get => "https://example.com/product.schema.json"; }
  [JsonPropertyName("$schema")]
  public string Schema { get => "https://json-schema.org/draft/2020-12/schema"; }
  public string Type { get => "object"; }
}