using Bus.Core.Models;
using Bus.Core.Repositories.Contract;
using Bus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Infrastructure
{
	public class AuthRepository : IAuthRepository<User>
	{

		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthRepository(ApplicationDbContext context, IConfiguration configuration)
		{

			_context = context;
			_configuration = configuration;
		}

		public async Task<string> Login(string email, string password)
		{
			var user = await _context.Users.Include(U=>U.Role).FirstOrDefaultAsync(U=>U.Email==email);


			if (user == null)
			{
				return null;
			}


			if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
			{
				return null;
			}
			var claims = new[]
			{
			  new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			 new Claim(ClaimTypes.Email, user.Email),
			 new Claim(ClaimTypes.Name, user.Name),
			  new Claim(ClaimTypes.Role, user.Role.Name),
			 };

			var key = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(_configuration.GetSection("Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				NotBefore = DateTime.Now,
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenToCreate = tokenHandler.CreateToken(tokenDescriptor);

			var token = tokenHandler.WriteToken(tokenToCreate);


			return token;
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computedHash.Length; i++)
				{
					if (computedHash[i] != passwordHash[i])
						return false;
				}
			}
			return true;
		}

		public async Task<User> Register(User user, string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			user.PasswordHash = passwordHash;
			user.PasswordSalt = passwordSalt;

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();


			return user;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public async Task<bool> UserExist(string email)
		{
			if (await _context.Users.AnyAsync(x => x.Email == email))
				return true;

			return false;
		}



	}
}
