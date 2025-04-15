using LabManagementApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add controllers
builder.Services.AddControllers();

// Register LabManagementDbContext with dependency injection
var connectionString = builder.Configuration.GetConnectionString("LabManagementDb");

// Validate the connection string
if (string.IsNullOrEmpty(connectionString))
{
  throw new InvalidOperationException("The connection string for 'LabManagementDb' is not configured.");
}

builder.Services.AddDbContext<LabManagementDbContext>(options =>
    options.UseSqlite(connectionString));

// Add Swagger services
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.MapOpenApi();
}

// Map controllers
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
