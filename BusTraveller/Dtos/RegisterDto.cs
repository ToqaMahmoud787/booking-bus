using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "User name is required")]
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		[EmailAddress]

		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Address is required")]
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		public string Address { get; set; }
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		[Phone]
		public string? PhoneNumber { get; set; }
		[Required]

		public int? UserRoleId { get; set; } = 2;
	}
}
