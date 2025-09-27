using productapi.model;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products/{id}/configuration/schema", IResult (string id) =>
{
    var schema = new InsuranceProduct
    {
        InsuranceProductId = id,
        InsuranceProductName = "Pensionsförsäkring",
        InsuranceProductDescription = "Some product description",
        Parameters = [new PayoutStartAge { Minimum = 55, Maximum = 65 }],
    };

    return Results.Ok(schema);
});

app.Run();
