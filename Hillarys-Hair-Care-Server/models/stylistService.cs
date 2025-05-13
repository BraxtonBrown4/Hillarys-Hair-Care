namespace HillarysHairCare.models;

public class StylistService {
    public int Id {get; set;}
    public int StylistId {get; set;}
    public int ServiceId {get; set;}
    public Service Service {get; set;}
}