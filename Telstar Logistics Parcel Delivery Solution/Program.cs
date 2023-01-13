using Microsoft.Extensions.DependencyInjection.Extensions;
using Telstar_Logistics_Parcel_Delivery_Solution.Calculations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("connectionstrings.json", optional: false, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<RouteMapperService, RouteMapperServiceImpl>();
builder.Services.AddScoped<PriceService, PriceServiceImpl>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
