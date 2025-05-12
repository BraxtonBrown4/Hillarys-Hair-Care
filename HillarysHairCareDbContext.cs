using HillarysHairCare.models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
public class HillarysHairCareDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Stylist> Stylists { get; set; }
    public DbSet<AppointmentService> AppointmentServices { get; set; }
    public DbSet<StylistService> StylistServices { get; set; }

    public HillarysHairCareDbContext(DbContextOptions<HillarysHairCareDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for Services
        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Name = "Haircut", Price = 25.00M, DurationInMinutes = 30 },
            new Service { Id = 2, Name = "Hair Coloring", Price = 75.00M, DurationInMinutes = 120 },
            new Service { Id = 3, Name = "Blow Dry", Price = 20.00M, DurationInMinutes = 20 },
            new Service { Id = 4, Name = "Hair Treatment", Price = 50.00M, DurationInMinutes = 60 }
        });

        // Seed data for Stylists
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, Name = "Hillary", ExperienceInYears = 10 },
            new Stylist { Id = 2, Name = "Alex", ExperienceInYears = 5 },
            new Stylist { Id = 3, Name = "Jordan", ExperienceInYears = 7 }
        });

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890" },
            new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "987-654-3210" }
        });

        // Seed data for Appointments
        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
            new Appointment { Id = 1, CustomerId = 1, StylistId = 1, Date = new DateTime(2023, 10, 1, 10, 0, 0) },
            new Appointment { Id = 2, CustomerId = 2, StylistId = 2, Date = new DateTime(2023, 10, 2, 14, 0, 0) },
            new Appointment { Id = 3, CustomerId = 1, StylistId = 3, Date = new DateTime(2023, 10, 3, 16, 0, 0) }
        });

        // Seed data for AppointmentService
        modelBuilder.Entity<AppointmentService>().HasData(new AppointmentService[]
        {
            new AppointmentService { AppointmentId = 1, ServiceId = 1 },
            new AppointmentService { AppointmentId = 1, ServiceId = 3 },
            new AppointmentService { AppointmentId = 2, ServiceId = 2 },
            new AppointmentService { AppointmentId = 3, ServiceId = 4 }
        });

        // Seed data for StylistService
        modelBuilder.Entity<StylistService>().HasData(new StylistService[]
        {
            new StylistService { StylistId = 1, ServiceId = 1 },
            new StylistService { StylistId = 1, ServiceId = 4 },
            new StylistService { StylistId = 2, ServiceId = 2 },
            new StylistService { StylistId = 3, ServiceId = 3 },
            new StylistService { StylistId = 3, ServiceId = 4 }
        });
    }
}