namespace CityManagerApiLesson13.Entities;
public class User
{
    // public properties : 
    public int Id { get; set; } 
    public string ? Username { get; set; }
    public byte[] ? PasswordHash { get; set; }
    public byte[] ? PasswordSalt { get; set; }

    // navigation properties : 
    public virtual ICollection<City> ? Cities { get; set; }
}
