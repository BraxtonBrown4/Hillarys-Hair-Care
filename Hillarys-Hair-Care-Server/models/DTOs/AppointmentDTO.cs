using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models.DTOs;

public class AppointmentDTO {
    public int Id {get; set;}
    public int CustomerId {get; set;}
    public int StylistId {get; set;}
    public DateTime Date {get; set;}
    public CustomerDTO? Customer {get; set;}
    public StylistDTO? Stylist {get; set;}
    public List<ServiceDTO>? Services {get; set;} 
}