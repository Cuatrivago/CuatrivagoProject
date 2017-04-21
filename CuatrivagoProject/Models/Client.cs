using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Models
{
    public class Client
    {

        public int id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public int creditCard { get; set; }
        public int idCard { get; set; }
        public Client(int id_, string name_, string lastName_, string email_, int phone_, int creditCard_, int idCard_)

        {
            this.id = id_;
            this.name = name_;
            this.lastName = lastName_;
            this.email = email_;
            this.phone = phone_;
            this.idCard = idCard_;
            this.creditCard = creditCard_;
        }
        public Client()

        {
            this.id =0;
            this.name = "";
            this.lastName ="";
            this.email = "";
            this.phone =0;
            this.idCard = 0;
            this.creditCard = 0;

        }
    }
}