using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public DateTime currentDate { get; set; }
        public DateTime inDate { get; set; }
        public DateTime outDate { get; set; }
        public int idClient { get; set; }
        public string reservationNumber { get; set; }
    }
}