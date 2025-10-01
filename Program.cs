using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using productapi.model;
using productapi.validation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/products/{id}/configuration/validate", async Task<IResult> (string id, [FromBody] JsonElement config) =>
{
    return Results.Ok(await ProductConfigValidation.ValidateProductConfiguration(config));
});

app.MapPost("/products/{id}/configuration/schema", async Task<IResult> (string id, [FromBody] JsonElement config) =>
{
    var validationResult = await ProductConfigValidation.ValidateProductConfiguration(config);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult);
    }

    var schema = new InsuranceProduct
    {
        InsuranceProductId = id,
        InsuranceProductName = "Pensionsförsäkring",
        InsuranceProductDescription = "Some product description",
        Parameters = [
            new SurvivorProtection { Value = true },
            new PremiumAmount { Minimum = 100, Maximum = 100000, Default = 1000 },
            new PayoutStartAge { Minimum = 55, Maximum = 75, Default = 65 }
        ],
    };

    return Results.Ok(schema);
});

app.Run();
