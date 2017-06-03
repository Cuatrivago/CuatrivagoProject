using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Models
{
    public class Image
    {
        public int idImage { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public int idObject { get; set; }
    }
}