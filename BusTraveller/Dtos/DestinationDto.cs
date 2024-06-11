using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
	public class DestinationDto
	{

		[Required(ErrorMessage = "Location is required")]
		[MaxLength(20, ErrorMessage = "Max length is 20")]
		public string Location { get; set; }
    }
}
