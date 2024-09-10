using CityManagerApiLesson13.Data.Abstractl;
using CityManagerApiLesson13.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CityManagerApiLesson13.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CityManagerApiLesson13.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    // private fields for injection : 
    private readonly IAppRepository _appRepository;
    private readonly IMapper _mapper;

    // constructor for injecting repository : 
    public CityController(IAppRepository appRepository,IMapper mapper)
    {
        _appRepository = appRepository;
        _mapper = mapper;
    }

    // GET: api/<CityController>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCities(int id)
    {
        //var dtos = (await _appRepository.GetCitiesAsync(id))
        //     .Select(c =>
        //     {
        //         return new CityForListDTO
        //         {
        //             Description = c.Description,
        //             Id = c.Id,
        //             Name = c.Name,
        //             PhotoUrl = c.CityImages?.FirstOrDefault(p => p.IsMain)?.Url
        //         };
        //     });
        //return Ok(dtos);
        var items = await _appRepository.GetCitiesAsync(id);
        var dtos = _mapper.Map<IEnumerable<CityForListDTO>>(items);
        return Ok(dtos);
    }


    // POST api/<CityController>
    [HttpPost("Add")]
    public async Task<IActionResult> Post([FromBody] CityDTO cityDTO)
    {
        // getting input mapping it to city and saving to db : 
        var entity = _mapper.Map<City>(cityDTO);
        await _appRepository.AddAsync(entity);
        await _appRepository.SaveAllAsync();

        // getting again city from db and mapping it to citydto : 
        var returnedValue = _mapper.Map<CityDTO>(entity);
        return Ok(returnedValue);
    }

    // PUT and DELETE WILL NOT WORK BECAUSE OF FOREIGN KEYS IN DB : 

    //// PUT api/<CityController>/5
    //[HttpPut("Change/{id}")]
    //public async Task<IActionResult> Put([FromBody] CityDTO cityDTO,int id)
    //{
    //    // getting city from db for changing it : 
    //    var entity =await  _appRepository.GetCityByIdAsync(id);
    //    if (entity == null) return NotFound("The City Was not found !");
    //    entity.Description = cityDTO.Description;
    //    entity.Name = cityDTO.Name;
    //    entity.UserId = cityDTO.UserId;

    //    await _appRepository.UpdateAsync(entity);
    //    await _appRepository.SaveAllAsync();

    //    // getting again city from db and mapping it to citydto : 
    //    var returnedValue = _mapper.Map<CityDTO>(entity);
    //    return Ok(returnedValue);
    //}

    //// DELETE api/<CityController>/5
    //[HttpDelete("Delete{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var entity = _appRepository.GetCityByIdAsync(id);
    //    if (entity == null) return NotFound("The City which u want to find was not found !");
    //    await _appRepository.DeleteAsync(entity);
    //    await _appRepository.SaveAllAsync();
    //    return NoContent();
    //}
}
