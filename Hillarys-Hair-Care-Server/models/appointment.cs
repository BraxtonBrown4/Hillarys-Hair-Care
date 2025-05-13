using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models;

public class Appointment {
    public int Id {get; set;}
    public int CustomerId {get; set;}
    public Customer? Customer {get; set;}
    public Stylist? Stylist {get; set;}
    public int StylistId {get; set;}
    public DateTime Date {get; set;}
    public List<AppointmentService>? AppointmentServices {get; set;}
    
}