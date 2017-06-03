using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Models
{
    public class Hotel
    {
        [Key]
        public int idHotel { get; set; }


        public string name { get; set; }
        public string homeInformation { get; set; }
        public string aboutUs { get; set; }

        public string address_ { get; set; }
        public string phone { get; set; }
        public string postMail { get; set; }
        public string email { get; set; }

    }
}