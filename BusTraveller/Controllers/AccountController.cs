using AutoMapper;
using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using BusTraveller.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTraveller.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthRepository<User> _authRepo;
		private readonly IDataRepository<User> _userRepo;
		private readonly IDataRepository<Role> _userRoleRepo;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;

		public AccountController(IAuthRepository<User> authRepo, IDataRepository<User> userRepo, IDataRepository<Role> userRoleRepo, IConfiguration configuration, IMapper mapper)
		{
			_authRepo = authRepo;
			_userRepo = userRepo;
			_userRoleRepo = userRoleRepo;
			_configuration = configuration;
			_mapper = mapper;
		}
		
		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginDto userForLoginDto)
		{
			var token = await _authRepo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

			if (token == null)
			{
				return Unauthorized("incorrect Email and Password");
			}

			return Ok(token);

		}


		[HttpGet("getAllRoles")]
		public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAllRoles()
		{
			var roles = await _userRoleRepo.GetAll();

			var returnedRoles = _mapper.Map<IEnumerable<UserRoleDto>>(roles);
			return Ok(returnedRoles);
		}
		[HttpPost("register")]
		public async Task<ActionResult<User>> Register([FromBody] RegisterDto userForRegisterDto)
		{

			userForRegisterDto.Email = userForRegisterDto.Email.ToLower();
			if (await _authRepo.UserExist(userForRegisterDto.Email))
			{
				return BadRequest("Email already exists");
			}

			var mappedUser = _mapper.Map<User>(userForRegisterDto);

			var user = await _authRepo.Register(mappedUser, userForRegisterDto.Password);

			return Ok(user);

		}
	}
}
