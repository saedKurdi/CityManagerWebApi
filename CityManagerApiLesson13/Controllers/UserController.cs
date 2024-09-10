using AutoMapper;
using CityManagerApiLesson13.Data.Abstractl;
using CityManagerApiLesson13.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerApiLesson13.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // private fields for injecting : 
    private readonly IAppRepository _appRepository;
    private readonly IMapper _mapper;

    // parametric constructor for injecting :
    public UserController(IAppRepository appRepository,IMapper mapper)
    {
        _appRepository = appRepository;
        _mapper = mapper;
    }

    // GET: api/<UserController>
    [HttpGet("All")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _appRepository.GetAllUsersAsync();
        if (users.Count == 0) return NotFound("User count in db is 0 !");
        var dtos = _mapper.Map<List<UserForListDTO>>(users);
        return Ok(dtos);
    }
}
