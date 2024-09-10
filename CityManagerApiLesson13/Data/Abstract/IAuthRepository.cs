using CityManagerApiLesson13.Entities;

namespace CityManagerApiLesson13.Data.Abstract;
public interface IAuthRepository
{
    // methods which will be implemented in classes : 
    Task<User> Register(User user,string password);
    Task<User> Login(string username,string password);
    Task<bool> UserExsists(string username);
}
