using Microsoft.OpenApi.Models;
using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var cs = builder.Configuration.GetConnectionString("Flights");

// Add DbContext.
builder.Services.AddDbContext<Entities>(options =>
  options.UseMySql(cs, ServerVersion.AutoDetect(cs))
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(config =>
    {
      config.AddServer(new OpenApiServer
      {
        Description = "Development Server",
        Url = "http://localhost:5037"
      });

      config.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]
      + e.ActionDescriptor.RouteValues["controller"]}");
    });

//builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

entities.Database.EnsureCreated();

var random = new Random();

if (!entities.Flights.Any())
{
 Flight[] flightsToSeed = new Flight[]{
  new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 3))),
            new TimePlace("Vienna",DateTime.Now.AddHours(random.Next(4, 10))),
            2,
            random.Next(2000, 5000).ToString()
        ),

        new (
            Guid.NewGuid(),
            "Lufthansa",
            new TimePlace("Berlin",DateTime.Now.AddHours(random.Next(1, 10))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 15))),
            random.Next(1, 853),
            random.Next(2000, 5000).ToString()
            ),

        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Changsha",DateTime.Now.AddHours(random.Next(4, 18))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
            "Air China",
            new TimePlace("Beijing",DateTime.Now.AddHours(random.Next(1, 21))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 21))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),

        new (
            Guid.NewGuid(),
             "China Eastern",
            new TimePlace("Shanghai",DateTime.Now.AddHours(random.Next(1, 23))),
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(4, 25))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Changsha",DateTime.Now.AddHours(random.Next(1, 15))),
            new TimePlace("Shanghai",DateTime.Now.AddHours(random.Next(4, 19))),
            random.Next(1, 853),
            random.Next(300, 800).ToString()
            ),


        new (
            Guid.NewGuid(),
            "Hainan Airlines",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 55))),
            new TimePlace("Budapest",DateTime.Now.AddHours(random.Next(4, 58))),
            random.Next(1, 853),
            random.Next(2000, 4000).ToString()
            ),


        new (
            Guid.NewGuid(),
            "China Sountern",
            new TimePlace("Guangzhou",DateTime.Now.AddHours(random.Next(1, 58))),
            new TimePlace("Munich",DateTime.Now.AddHours(random.Next(4, 60))),
            random.Next(1, 853),
            random.Next(2000, 4000).ToString()
            )
};

  entities.Flights.AddRange(flightsToSeed);

  entities.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseCors(builder =>
  builder.WithOrigins("*")
  .AllowAnyHeader()
  .AllowAnyMethod()
);

app.UseHttpsRedirection();

app.MapControllers();

app.UseSwagger().UseSwaggerUI();

app.Run();
