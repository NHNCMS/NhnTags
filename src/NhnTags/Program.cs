using FluentValidation.AspNetCore;
using NhnTags.Modules;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapEndpoints();

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
    s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/documentation/v1/documentation.json", "NhN Tags Api");
    s.RoutePrefix = "documentation";
});

app.Run();