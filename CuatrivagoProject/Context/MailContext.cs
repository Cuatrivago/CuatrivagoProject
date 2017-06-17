using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class MailContext
    {   //constructor
        private SmtpClient server;
        public MailContext()
        {
            /*
        * Cliente SMTP
        * Gmail:  smtp.gmail.com  puerto:587
        * Hotmail: smtp.liva.com  puerto:25
        */
            //this.server = new SmtpClient("smtp.gmail.com", 587);
            this.server = new SmtpClient("smtp.gmail.com", 587);

            server.Credentials = new System.Net.NetworkCredential("cuatrivagoucr@gmail.com", "ucrcuatrivago");
            server.EnableSsl = true;
        }
        //envia mensaje
        public int sendMail(MailMessage mail)
        {
            try
            {
                server.Send(mail);
                
                mail.Dispose();

                Debug.WriteLine("enviado111111111111111111111111111111111111111111111111111111111");
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("enviado9999999999999999999999999999999999999999999999111");
                Debug.WriteLine(ex.ToString());
                return 0;
            }
        }
    }
}