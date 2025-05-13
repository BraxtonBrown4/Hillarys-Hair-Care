using HillarysHairCare.models.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HillarysHairCareDbContext>(builder.Configuration["posgresServerString"]);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/styles", (HillarysHairCareDbContext db) => {
    return db.Stylists
    .Include(s => s.StylistServices)
    .ThenInclude(ss => ss.Service)
    .Select(stylist => new StylistDTO {
            Id = stylist.Id,
            Name = stylist.Name,
            IsActive = stylist.IsActive,
            Services = stylist.StylistServices.Select(s => new ServiceDTO {
                Id = s.Service.Id,
                Name = s.Service.Name,
                Price = s.Service.Price
            }).ToList()
    });
});

app.MapGet("/customers", (HillarysHairCareDbContext db) => {
    return db.Customers.Select(customer => new CustomerDTO {
            Id = customer.Id,
            Name = customer.Name,
            Balance = customer.Balance
    });
});

app.Run();