using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models;

public class Customer {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public float Balance {get; set;}
}