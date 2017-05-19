using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Models
{
    public class Room
    {
        public int idRoom { get; set; }
        public int capacity { get; set; }
        public string description_ { get; set; }
        public int roomType { get; set; }
        public int price { get; set; }
    }
}