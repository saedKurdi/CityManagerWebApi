namespace CityManagerApiLesson13.DTO;
public class CityImageDTO
{
    // public properties : 
    public int Id { get; set; } 
    public string? Url { get; set; }
    public string? Description { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsMain { get; set; }

    // foreign keys : 
    public int CityId { get; set; }
}
