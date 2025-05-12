using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models;

public class Service {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public float Price {get; set;}
}