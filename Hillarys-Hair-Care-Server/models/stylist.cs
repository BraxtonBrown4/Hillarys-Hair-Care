using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models;

public class Stylist {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public bool IsActive {get; set;}
    public List<StylistService> StylistServices {get; set;}
}