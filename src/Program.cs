using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using productapi.rules;
using productapi.validation;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/products/{prodcutId}/configuration/validate", async Task<IResult> (string prodcutId, [FromBody] JsonElement config) =>
{
    return Results.Ok(await ProductConfigValidation.ValidateProductConfiguration(config));
});

app.MapPost("/products/{prodcutId}/configuration/schema", async Task<IResult> (string prodcutId, [FromBody] JsonElement config) =>
{
    var validationResult = await ProductConfigValidation.ValidateProductConfiguration(config);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult);
    }

    // parameters set by the user
    var spec = ProductConfigValidation.JsonToParameters(config);

    // get age from jwt
    var birthDate = spec.BirthDate;

    // get this stuff from Lumera
    var lumeraStuff = new LumeraSettings
    {
        InsuranceProductName = "Pensionsförsäkring",
        InsuranceProductDescription = "Pensionsförsäkring",
        TaxCategory = spec.TaxCategory,
        Z = spec.Z,
        Maturity = spec.Maturity,
        BenefitProductId = spec.BenefitProductId
    };

    var engine = new RulesEngine(prodcutId, birthDate, lumeraStuff);
    var schema = engine.GetProductConfigurationSchema(spec);

    return Results.Ok(schema);
});

app.Run();
