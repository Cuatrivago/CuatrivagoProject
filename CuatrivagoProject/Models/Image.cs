using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class Image
    {
        public int id { get; set; }
        public string description { get; set; }
        public string path { get; set; }

        public Image(int id_, string description_, string path_)
        {
            this.id = id_;
            this.description = description_;
            this.path = path_;
        }
        public Image()
        {
            this.id = 0;
            this.description = "";
            this.path = "";
        }
    }
}