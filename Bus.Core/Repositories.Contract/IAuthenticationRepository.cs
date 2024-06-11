using Bus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Core.Repositories.Contract
{
	
		public interface IAuthRepository<T> where T : User
		{
			Task<User> Register(User user, string password);
			Task<string> Login(string email, string password);
			Task<bool> UserExist(string username);

		}
	}

