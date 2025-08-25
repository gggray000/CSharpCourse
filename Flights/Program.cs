using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen( config =>
    {
      config.AddServer(new OpenApiServer
      {
        Description = "Development Server",
        Url = "http://localhost:5037 "
      });

      config.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]
      + e.ActionDescriptor.RouteValues["controller"]}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(builder => builder.WithOrigins("*"));

app.UseHttpsRedirection();

app.MapControllers();

app.UseSwagger().UseSwaggerUI();

app.Run();
