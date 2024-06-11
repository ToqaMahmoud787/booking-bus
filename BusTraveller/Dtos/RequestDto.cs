using System.ComponentModel.DataAnnotations;

namespace BusTraveller.Dtos
{
    public class RequestDto
    {
        [Required(ErrorMessage = "traveller id is required")]
     
        public int TravellerId { get; set; }


        [Required(ErrorMessage = "destination id  is required")]
      
        public int DestinationId { get; set; }


        [Required(ErrorMessage = "appointment id is required")]
 
        public int AppointmentId { get; set; }


        [Required(ErrorMessage = "status is required")]
    
        public bool Status { get; set; }
     
    }
}
