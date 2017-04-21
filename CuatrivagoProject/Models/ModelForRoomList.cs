using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class ModelForRoomList
    {
        public List<Room> roomList { get; set; }
        public int back { get; set; }
        public string dateIn { get; set; }
        public string dateOut { get; set; }
    }
}