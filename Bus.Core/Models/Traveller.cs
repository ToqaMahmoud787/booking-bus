using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.Core.Models
{
	public class Traveller:User
	{
        public ICollection<Request> Requests { get; set; } = new HashSet<Request>();
        public TravellerHistorySearch TravellerHistorySearchs { get; set; } 
    }
}
