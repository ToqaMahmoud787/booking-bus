using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Core.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public string? PhoneNumber { get; set; }

		public int? UserRoleId { get; set; }

		public Role Role { get; set; }

		public bool Status { get; set; }

		public string Address { get; set; }



	}
}
