namespace CityManagerApiLesson13.Entities;
public class CityImage
{
    // public properties : 
    public int Id { get; set; }
    public string ? Url { get; set; }
    public string ? Description { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsMain { get; set; }

    // foreign keys : 
    public int CityId { get; set; }

    // navigation properties : 
    public virtual City ? City { get; set; }
}
