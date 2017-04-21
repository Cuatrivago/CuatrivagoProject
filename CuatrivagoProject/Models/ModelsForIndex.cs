using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class ModelsForIndex
    {
        public Hotel Hotel { get; set; }
        public List<Advertisement> advertisement { get; set; }
        public List<Facilitie> facilitie { get; set; }
        public List<RoomType> roomType { get; set; }
        public List<Image> image { get; set; }
    }
}