using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Models
{
    public class StatusHotel
    {
        public string name { get; set; }
        public int quantity { get; set; }
        public int reserve { get; set; }
        public int blocked { get; set; }
    }
}