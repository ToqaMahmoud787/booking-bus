using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
	public class LoginDto
	{
		[EmailAddress]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "passworda is required")]

		public string Password { get; set; }
	}
}
