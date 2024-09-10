using CityManagerApiLesson13.Entities;

namespace CityManagerApiLesson13.Data.Abstractl;
public interface IAppRepository
{
    // methods which will be implemented in other classes : 
    Task AddAsync<T>(T entity) where T :class;
    Task DeleteAsync<T>(T entity) where T :class;
    Task UpdateAsync<T>(T entity) where T :class;
    Task<bool> SaveAllAsync();
    Task<List<City>> GetCitiesAsync(int userId);
    Task<CityImage> GetPhotoByCityIdAsync(int cityId);
    Task<City> GetCityByIdAsync(int cityId);
    Task<CityImage> GetPhotoByIdAsync(int photoId);
    Task<List<User>> GetAllUsersAsync();
    Task<List<CityImage>> GetAllImagesAsync();
    Task<List<CityImage>> GetImagesByCityId(int id);
}
