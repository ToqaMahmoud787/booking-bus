using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using BusTraveller.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTraveller.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IDataRepository<Role> _roleRepo;

		public RoleController(IDataRepository<Role> roleRepo)
		{
			_roleRepo = roleRepo;
		}


		[HttpPost("create")]
		public async Task<ActionResult> CreateUserType(UserRoleDto userRole)
		{
			if (userRole == null)
			{
				return BadRequest();
			}

			var role = new Role()
			{
				Name = userRole.Name
			};

			await _roleRepo.Add(role);

			return Ok();


		}
	}
}
