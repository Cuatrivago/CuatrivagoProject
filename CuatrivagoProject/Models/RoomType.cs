using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class RoomType
    {
        public int idRoomType { get; set; }
        public string name { get; set; }
        public string description_ { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}