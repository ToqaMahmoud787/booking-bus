using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bus.Core.Models
{
  
    public class TravellerHistorySearch
    {
        public int Id { get; set; }
        public string startPoint { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
       

        public int TravellerId { get; set; }
        public Traveller Traveller { get; set; }
       
    }
}
