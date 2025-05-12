using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models;

public class Appointment {
    public int Id {get; set;}
    public int CustomerId {get; set;}
    public int StylistId {get; set;}
    public DateTime Date {get; set;}
}