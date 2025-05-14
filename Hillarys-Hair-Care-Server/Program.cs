using HillarysHairCare.models;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapGet("/stylists", (HillarysHairCareDbContext db) =>
{
    return db.Stylists
    .Include(s => s.StylistServices)
    .ThenInclude(ss => ss.Service)
    .Select(stylist => new StylistDTO
    {
        Id = stylist.Id,
        Name = stylist.Name,
        IsActive = stylist.IsActive,
        Services = stylist.StylistServices.Select(s => new ServiceDTO
        {
            Id = s.Service.Id,
            Name = s.Service.Name,
            Price = s.Service.Price
        }).ToList()
    });
});

app.MapGet("/customers", (HillarysHairCareDbContext db) =>
{
    return db.Customers.Select(customer => new CustomerDTO
    {
        Id = customer.Id,
        Name = customer.Name,
        Balance = customer.Balance
    });
});

app.MapGet("/appointments", (HillarysHairCareDbContext db) =>
{
    return db.Appointments
    .Include(a => a.Stylist)
    .Include(a => a.Customer)
    .Include(a => a.AppointmentServices)
    .ThenInclude(a => a.Service)
    .Select(appointment => new AppointmentDTO
    {
        Id = appointment.Id,
        Date = appointment.Date,
        Stylist = new StylistDTO
        {
            Id = appointment.Stylist.Id,
            Name = appointment.Stylist.Name,
            IsActive = appointment.Stylist.IsActive
        },
        Customer = new CustomerDTO
        {
            Id = appointment.Customer.Id,
            Name = appointment.Customer.Name,
            Balance = appointment.Customer.Balance
        },
        Services = appointment.AppointmentServices.Select(s => new ServiceDTO
        {
            Id = s.Service.Id,
            Name = s.Service.Name,
            Price = s.Service.Price
        }).ToList()
    });
});

app.MapPost("/appointments", (HillarysHairCareDbContext db, Appointment appointment) =>
{
    try
    {
        db.Appointments.Add(appointment);
        db.SaveChanges();
        
        return Results.Created($"/appointments/{appointment.Id}", new AppointmentDTO {
            Id = appointment.Id,
            Date = appointment.Date,
            StylistId = appointment.StylistId,
            CustomerId = appointment.CustomerId
        });
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

app.MapPut("/appointments/{id}", (HillarysHairCareDbContext db, int id, Appointment putA) => {
    Appointment appointment = db.Appointments.FirstOrDefault(a => a.Id == id);

    if (appointment == null) {
        return Results.NotFound();
    }

    try
    {
        appointment.Date = putA.Date;
        appointment.StylistId = putA.StylistId;
        appointment.CustomerId = putA.CustomerId;

        db.SaveChanges();

        return Results.Ok(new AppointmentDTO
        {
            Id = appointment.Id,
            Date = appointment.Date,
            StylistId = appointment.StylistId,
            CustomerId = appointment.CustomerId
        });
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

app.MapPost("/appointmentServices", (HillarysHairCareDbContext db, AppointmentService appointmentService) => {
    try
    {
        AppointmentService a = db.AppointmentServices.SingleOrDefault(a => a.AppointmentId == appointmentService.AppointmentId && a.ServiceId == appointmentService.Id);

        if (a != null) {
            return Results.BadRequest("jointable already exists");
        }
        db.AppointmentServices.Add(appointmentService);
        db.SaveChanges();

        return Results.Created($"/appointmentServices/{appointmentService.Id}", new AppointmentServiceDTO
        {
            Id = appointmentService.Id,
            AppointmentId = appointmentService.AppointmentId,
            ServiceId = appointmentService.ServiceId
        });
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

app.MapDelete("/appointments/{id}", (HillarysHairCareDbContext db, int id) => {
        Appointment a = db.Appointments.FirstOrDefault(a => a.Id == id);
        if (a == null) {
            return Results.BadRequest("jointable already exists");
        }

    db.Appointments.Remove(a);
    db.SaveChanges();
    return Results.Accepted();
});

app.MapPut("/appointmentServices", (HillarysHairCareDbContext db, AppointmentService da) => {
    AppointmentService a = db.AppointmentServices.FirstOrDefault(a => a.ServiceId == da.ServiceId && a.AppointmentId == da.AppointmentId);

    if (a == null) {
        return Results.NotFound();
    }

    db.AppointmentServices.Remove(a);
    db.SaveChanges();
    return Results.Accepted();
});

app.MapPut("/appointmentServices/{id}", (HillarysHairCareDbContext db, int id, AppointmentService putA) => {
    AppointmentService appointmentService = db.AppointmentServices.FirstOrDefault(a => a.Id == id);

    if (appointmentService == null) {
        return Results.NotFound();
    }

    try
    {
        appointmentService.ServiceId = putA.ServiceId;
        appointmentService.AppointmentId = putA.AppointmentId;

        db.SaveChanges();

        return Results.Ok(new AppointmentServiceDTO
        {
            Id = appointmentService.Id,
            AppointmentId = appointmentService.AppointmentId,
            ServiceId = appointmentService.ServiceId
        });
    }
    catch (DbUpdateException)
    {
        return Results.BadRequest("Invalid data submitted");
    }
});

app.Run();

