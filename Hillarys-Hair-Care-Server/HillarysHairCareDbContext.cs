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

    public HillarysHairCareDbContext(DbContextOptions<HillarysHairCareDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for Services
        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Name = "Haircut", Price = 25.00F },
            new Service { Id = 2, Name = "Hair Coloring", Price = 75.00F },
            new Service { Id = 3, Name = "Blow Dry", Price = 20.00F },
            new Service { Id = 4, Name = "Hair Treatment", Price = 50.00F }
        });

        // Seed data for Stylists
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, Name = "Hillary", IsActive = true },
            new Stylist { Id = 2, Name = "Alex", IsActive = true },
            new Stylist { Id = 3, Name = "Jordan", IsActive = true }
        });

        // Seed data for Customers
        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer { Id = 1, Name = "John Doe", Balance = 100.00F },
            new Customer { Id = 2, Name = "Jane Smith", Balance = 50.00F }
        });

        // Seed data for Appointments
        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
            new Appointment { Id = 1, CustomerId = 1, StylistId = 1, Date = new DateTime(2025, 11, 15, 9, 30, 0) },
            new Appointment { Id = 2, CustomerId = 2, StylistId = 2, Date = new DateTime(2025, 11, 16, 13, 45, 0) },
            new Appointment { Id = 3, CustomerId = 1, StylistId = 3, Date = new DateTime(2025, 11, 17, 11, 0, 0) }
        });

        // Seed data for AppointmentService
        modelBuilder.Entity<AppointmentService>().HasData(new AppointmentService[]
        {
            new AppointmentService { Id = 1, AppointmentId = 1, ServiceId = 1 },
            new AppointmentService { Id = 2, AppointmentId = 1, ServiceId = 3 },
            new AppointmentService { Id = 3, AppointmentId = 2, ServiceId = 2 },
            new AppointmentService { Id = 4, AppointmentId = 3, ServiceId = 4 }
        });

        // Seed data for StylistService
        modelBuilder.Entity<StylistService>().HasData(new StylistService[]
        {
            new StylistService { Id = 1, StylistId = 1, ServiceId = 1 },
            new StylistService { Id = 2, StylistId = 1, ServiceId = 4 },
            new StylistService { Id = 3, StylistId = 2, ServiceId = 2 },
            new StylistService { Id = 4, StylistId = 3, ServiceId = 3 },
            new StylistService { Id = 5, StylistId = 3, ServiceId = 4 }
        });
    }
}