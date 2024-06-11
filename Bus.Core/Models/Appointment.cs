using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bus.Core.Models
{
	public class Appointment
	{
        public int Id { get; set; }
        public string Date { get; set; }
        public DateTime DepartureTime { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }
        public int DestinationId { get; set; }
        [JsonIgnore]
        public Destination Destination { get; set; }

        public ICollection<Request> Requests { get; set; } = new HashSet<Request>();
    }
}
