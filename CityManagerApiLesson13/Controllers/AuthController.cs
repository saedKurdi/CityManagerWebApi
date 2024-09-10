using CityManagerApiLesson13.Data.Abstract;
using CityManagerApiLesson13.DTO;
using CityManagerApiLesson13.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityManagerApiLesson13.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    // private readonly fields for injecting : 
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    // parametric constructor for injected fields : 
    public AuthController(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDTO userRegisterDTO)
    {
        if(await _authRepository.UserExsists(userRegisterDTO.Username))
        {
            ModelState.AddModelError("Username", "'Username' already exists !");
        }
        if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
        }
        var userToCreate = new User
        {
            Username = userRegisterDTO.Username,
        };
        await _authRepository.Register(userToCreate,userRegisterDTO.Password);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDTO userLoginDTO)
    {
        var user = await _authRepository.Login(userLoginDTO.Username,userLoginDTO.Password);
        if(user == null)
        {
            //return StatusCode(StatusCodes.Status401Unauthorized);
            return Unauthorized();
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),

            }),
            Expires= DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return Ok(tokenString);
    }

}
