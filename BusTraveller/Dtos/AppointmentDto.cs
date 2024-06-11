using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
    public class AppointmentDto
    {
        public int destinationId { get; set; }

        [Required(ErrorMessage = "date is required")]
        public  string Date { get; set; }
        [Required(ErrorMessage = "DepatureTime is required")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "StartPoint is required")]
        [MaxLength(20, ErrorMessage = "Max length is 20")]
        public string StartPoint { get; set; }

        [Required(ErrorMessage = "EndPoint is required")]
        [MaxLength(20, ErrorMessage = "Max length is 20")]
        public string EndPoint { get; set; }

        [Required(ErrorMessage = "Capcity is required")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "price is required")]
        public decimal Price { get; set; }
    }
}
