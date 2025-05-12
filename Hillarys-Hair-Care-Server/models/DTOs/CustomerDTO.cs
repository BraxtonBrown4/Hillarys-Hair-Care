using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models.DTOs;

public class CustomerDTO {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public float Balance {get; set;}
}