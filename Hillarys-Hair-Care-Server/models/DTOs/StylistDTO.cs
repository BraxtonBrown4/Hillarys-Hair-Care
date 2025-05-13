using System.ComponentModel.DataAnnotations;

namespace HillarysHairCare.models.DTOs;

public class StylistDTO {
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public bool IsActive {get; set;}
    public List<ServiceDTO> Services {get; set;}

}