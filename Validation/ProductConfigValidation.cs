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

        var json = System.Text.Json.JsonSerializer.Serialize(config);
        var schema = await JsonSchema.FromFileAsync("./SimpleSchema.json");
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
        if (!IsPowerOfTwo(config.Ram))
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
        return number == 0 || number > 0 && (number & (number - 1)) == 0;
    }
}
