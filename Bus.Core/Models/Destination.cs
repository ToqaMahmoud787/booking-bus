using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bus.Core.Models
{
	public class Destination
	{
		public int Id { get; set; }
        public string Location { get; set; }

        public ICollection<Request> Requests { get; set; }=new HashSet<Request>();
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }= new HashSet<Appointment>();
    }
}
