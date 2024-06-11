using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
    public class TravellerHistorySearchDto
    {
        [Required(ErrorMessage = "StartPoint is required")]
        [MaxLength(20, ErrorMessage = "Max length is 20")]
        public string startPoint { get; set; }

        [Required(ErrorMessage = "MinPrice is required")]
        public decimal MinPrice { get; set; }

        [Required(ErrorMessage = "MaxPrice is required")]
        public decimal MaxPrice { get; set; }

        public int TravellerId { get; set; }
    }
}
