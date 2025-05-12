using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.DTOs;

public class ServiceDTO {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public float Price {get; set;}
}