using System.Text.Json;
using System.Text.Json.Serialization;
using NJsonSchema;
using productapi.model;

namespace productapi.validation;

public record ValidationResult
{
    public bool IsValid { get; init; }
    public ICollection<ValidationError> Errors { get; init; } = [];
}

public record ValidationError
{
    public string Path { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}

public static class ProductConfigValidation
{
    static JsonSerializerOptions serializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static async Task<ValidationResult> ValidateProductConfiguration(JsonElement config)
    {
        var errors = await ValidateJsonStructure(config);
        if (errors.Count == 0)
        {
            errors = ValidateSemantics(config);
            if (errors.Count == 0)
            {
                return new ValidationResult
                {
                    IsValid = true
                };
            }
        }

        return new ValidationResult
        {
            IsValid = false,
            Errors = errors
        };
    }

    public static InsuranceProductSpecification JsonToParameters(JsonElement config)
    {
        return config.Deserialize<InsuranceProductSpecification>(serializeOptions) ?? throw new Exception("Could not deserialize...");
    }

    private static async Task<ICollection<ValidationError>> ValidateJsonStructure(JsonElement config)
    {
        var validationErrors = new List<ValidationError>();

        var schema = await JsonSchema.FromFileAsync("./Schemas/ProductSchema.json");
        var errors = schema.Validate(config.ToString());

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
        }

        return validationErrors;
    }

    private static ICollection<ValidationError> ValidateSemantics(JsonElement config)
    {
        var validationErrors = new List<ValidationError>();

        var parameters = config.Deserialize<InsuranceProductSpecification>(serializeOptions) ?? throw new Exception("Could not deserialize...");

        //if (parameters.PremiumAmount.HasValue && !IsPowerOfTwo(parameters.PremiumAmount.Value))
        //{
        //    validationErrors.Add(new()
        //    {
        //        Path = "#/PremiumAmount",
        //        Message = "PremiumAmount must be a power of two."
        //    });
        //}

        return validationErrors;
    }

    private static bool IsPowerOfTwo(int number)
    {
        return number > 0 && (number & (number - 1)) == 0;
    }
}
