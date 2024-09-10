namespace CityManagerApiLesson13.Entities;
public class City
{
    // public properties : 
    public int Id { get; set; }
    public string ? Name { get; set; }
    public string? Description { get; set; }

    // foreign keys : 
    public int UserId { get; set; }

    // navigation properties : 
    public virtual User ? User { get; set; }
    public virtual ICollection<CityImage> ? CityImages { get; set; }
}
