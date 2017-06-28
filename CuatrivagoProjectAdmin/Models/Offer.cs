using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Models
{
    public class Offer
    {
        public int id { get; set; }
        public DateTime dateIn { get; set; }
        public DateTime dateOut { get; set; }
        public int discount { get; set; }
        public string roomType { get; set; }
        public int active { get; set; }
    }
}