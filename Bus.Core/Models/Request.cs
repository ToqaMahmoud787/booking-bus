namespace Bus.Core.Models
{

	public class Request
	{
        public int Id { get; set; }
        public bool Status { get; set; }

        public int TravellerId { get; set; }
        public Traveller Traveller { get; set; }

        public int DestinationId { get; set; }
        public Destination Destination { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}