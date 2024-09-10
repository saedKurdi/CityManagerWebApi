using CityManagerApiLesson13.Data.Abstractl;
using CityManagerApiLesson13.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityManagerApiLesson13.Data.Concrete;
public class AppRepository : IAppRepository
{
    // private fields : 
    private readonly AppDataContext _context;

    // parametric consturctor for injecting : 
    public AppRepository(AppDataContext context)
    {
        _context = context; 
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await _context.AddAsync(entity);
    }

    public async Task DeleteAsync<T>(T entity) where T : class
    {
        await Task.Run(() => {
            _context.Remove(entity);
        });
    }

    public async Task UpdateAsync<T>(T entity) where T : class
    {
        await Task.Run(() =>
        {
            _context.Update(entity);
        });
    }

    public async Task<List<City>> GetCitiesAsync(int userId)
    {
        var cities = await _context.Cities.Include(c => c.CityImages).Where(c => c.UserId == userId).ToListAsync();
        return cities;
    }

    public async Task<City> GetCityByIdAsync(int cityId)
    {
        var city = await _context.Cities
            .Include(c => c.CityImages)
            .FirstOrDefaultAsync(c=>c.Id == cityId);
        return city;
    }

    public async Task<CityImage> GetPhotoByCityIdAsync(int cityId)
    {
        var city = await _context.Cities.Include(c=>c.CityImages).FirstOrDefaultAsync(c => c.Id == cityId);
        return city.CityImages.FirstOrDefault(i => i.IsMain);
    }

    public async Task<CityImage> GetPhotoByIdAsync(int photoId)
    {
        var photo = await _context.CityImages.FirstOrDefaultAsync(i => i.Id == photoId);
        return photo;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await Task.Run(async () =>
        {
            return (await _context.SaveChangesAsync()) > 0;
        });
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await _context.Users.Include(u => u.Cities).ToListAsync();
        return users;
    }

    public async Task<List<CityImage>> GetAllImagesAsync()
    {
        var allImages = await _context.CityImages.ToListAsync();
        return allImages;
    }

    public async Task<List<CityImage>> GetImagesByCityId(int id)
    {
        var city = await _context.Cities.Include(c => c.CityImages).FirstOrDefaultAsync(c => c.Id == id);
        return city.CityImages.ToList();
    }
}
