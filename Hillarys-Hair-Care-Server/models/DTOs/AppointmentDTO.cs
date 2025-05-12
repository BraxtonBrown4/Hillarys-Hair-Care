using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models.DTOs;

public class AppointmentDTO {
    public int Id {get; set;}
    public int CustomerId {get; set;}
    public int StylistId {get; set;}
    public DateTime Date {get; set;}
}