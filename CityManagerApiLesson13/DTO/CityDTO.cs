namespace CityManagerApiLesson13.DTO;
public class CityDTO
{
    // public properties : 
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // foreign keys : 
    public int UserId { get; set; }
}
