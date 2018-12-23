using Microsoft.AspNetCore.Mvc;
using DatingApp.API.BLL.Auth;
using DatingApp.API.Data;
using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using Microsoft.Extensions.Configuration;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request

            if (await _repo.UserExists(userForRegisterDto.Username.ToLower()))
                return BadRequest($"Username {userForRegisterDto.Username} already exists");

            var createdUser = _repo.Resister(new User { Username = userForRegisterDto.Username.ToLower() }, userForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (userFromRepo == null)
                return Unauthorized();
            
            return Ok(new {
                token = AuthService.GetUserToken(userFromRepo,1,_config.GetSection("AppSettings:Token").Value)
            });

        }


    }
}