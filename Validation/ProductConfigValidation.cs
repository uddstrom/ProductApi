using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;
using productapi.model;

namespace productapi.validation;

public record ValidationError
{
    public string Path { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public static class ProductConfigValidation
{
    public static async Task<ICollection<ValidationError>> ValidateProductConfig(InsuranceProductRequest config)
    {
        var validationErrors = new List<ValidationError>();

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonSerializer.Serialize(config, serializeOptions);
        var schema = await JsonSchema.FromFileAsync("./Schemas/ProductSchema.json");
        var errors = schema.Validate(json);

        if (errors.Count > 0)
        {
            // Json structure invalid.
            foreach (var error in errors)
            {
                validationErrors.Add(new()
                {
                    Path = $"{error.Path}",
                    Message = $"{error.Kind} ({error.Property})"
                });
            }
            return validationErrors;
        }

        // Json structur valid, continue with semantic validation
        if (config.Ram.HasValue && !IsPowerOfTwo(config.Ram.Value))
        {
            validationErrors.Add(new()
            {
                Path = "#/Ram",
                Message = "Ram must be a power of two."
            });
        }

        return validationErrors;
    }

    private static bool IsPowerOfTwo(int number)
    {
        return number > 0 && (number & (number - 1)) == 0;
    }
}
