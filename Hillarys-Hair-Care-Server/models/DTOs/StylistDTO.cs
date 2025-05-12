using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.DTOs;

public class StylistDTO {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public bool IsActive {get; set;}
}