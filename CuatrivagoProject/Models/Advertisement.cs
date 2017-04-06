using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class Advertisement
    {

        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string path { get; set; }

    }
}