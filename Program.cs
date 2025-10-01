using Microsoft.AspNetCore.Mvc;
using productapi.model;
using productapi.validation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/products/{id}/configuration/validate", async Task<IResult> (string id, [FromBody] InsuranceProductRequest config) =>
{
    return Results.Ok(await ProductConfigValidation.ValidateProductConfig(config));
});

app.MapPost("/products/{id}/configuration/schema", async Task<IResult> (string id, [FromBody] InsuranceProductRequest config) =>
{
    var errors = await ProductConfigValidation.ValidateProductConfig(config);
    if (errors.Count > 0)
    {
        return Results.BadRequest(errors);
    }

    var schema = new InsuranceProduct
    {
        InsuranceProductId = id,
        InsuranceProductName = "Pensionsförsäkring",
        InsuranceProductDescription = "Some product description",
        Parameters = [
            new SurvivorProtection { Value = true },
            new PayoutStartAge { Minimum = 55, Maximum = 65 }
        ],
    };

    return Results.Ok(schema);
});

app.Run();
