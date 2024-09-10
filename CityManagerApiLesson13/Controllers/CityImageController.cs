using AutoMapper;
using CityManagerApiLesson13.Data.Abstractl;
using CityManagerApiLesson13.DTO;
using CityManagerApiLesson13.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerApiLesson13.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CityImageController : ControllerBase
{
    // private fields for injection : 
    private readonly IAppRepository _appRepository;
    private readonly IMapper _mapper;

    // constructor for injecting repository : 
    public CityImageController(IAppRepository appRepository,IMapper mapper)
    {
        _appRepository = appRepository;
        _mapper = mapper;
    }

    // GET: api/<CityImageController>
    [HttpGet("AllImages")]
    public async Task<IActionResult> GetAllImages()
    {
        // getting all city images from repository : 
        var images = await _appRepository.GetAllImagesAsync();
        if(images.Count == 0) return NotFound("There is not city images in DB !");
        var dtos = _mapper.Map<List<CityImageDTO>>(images);
        return Ok(dtos);
    }

    // GET api/<CityImageController>/5
    [HttpGet("MainImage/{id}")]
    public async Task<IActionResult> GetMainImageOfCity(int id)
    {
        var mainImage = await _appRepository.GetPhotoByCityIdAsync(id);
        var dto = _mapper.Map<CityImageDTO>(mainImage);
        return Ok(dto);
    }

    // GET api<CityImageController>/5
    [HttpGet("ImagesOfCity/{id}")]
    public async Task<IActionResult> GetAllImagesOfCity(int id)
    {
        var images =await _appRepository.GetImagesByCityId(id);
        var dtos = _mapper.Map<CityImageDTO[]>(images);
        return Ok(dtos);
    }

    // POST api/<CityImageController>
    [HttpPost("Add")]
    public async Task<IActionResult> Post([FromBody] CityImageDTO dto)
    {
        var entity = _mapper.Map<CityImage>(dto);
        await _appRepository.AddAsync(entity);
        return Ok(entity);  
    }
}
