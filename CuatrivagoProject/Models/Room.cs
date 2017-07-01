using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class Room
    {
        public int idRoom { get; set; }
        public int capacity { get; set; }
        public string colon { get; set; }
        public string dolar { get; set; }
        public string description_ { get; set; }
        public int roomType { get; set; }
        public List<Image> images { get; set; }
    }
}