using Microsoft.EntityFrameworkCore;
using MyApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("DefaultConnection"); 
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(connString));

builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Nuget Packages - SwashBuckle.AspNetCore 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
