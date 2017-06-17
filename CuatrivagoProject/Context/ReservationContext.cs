using CuatrivagoProject.Models;
using CuatrivagoProjectAdmin.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CuatrivagoProject.Context
{
    public class ReservationContext
    {
        private MailContext serverMail = new MailContext();
        public string insertReservation(string conn, string dateIn_, string dateOut_, int client_, int room_, string mail, string subscription)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Make_Reservation";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            DateTime thisDate1 = new DateTime(2011, 6, 10);

            SqlParameter currentDate = new SqlParameter();
            currentDate.ParameterName = "@DateR_";
            currentDate.SqlDbType = SqlDbType.VarChar;
            currentDate.Direction = ParameterDirection.Input;
            currentDate.Value = "2017-01-01 11:01:01.000";
            sqlCommand.Parameters.Add(currentDate);
            Debug.WriteLine("2017-01-01 11:01:01.000" + "\n");

            SqlParameter dateIn = new SqlParameter();
            dateIn.ParameterName = "@DateI_";
            dateIn.SqlDbType = SqlDbType.VarChar;
            dateIn.Direction = ParameterDirection.Input;
            dateIn.Value = dateIn_ + " 14:00:00.000";
            sqlCommand.Parameters.Add(dateIn);
            Debug.WriteLine(dateIn_ + " 14:00:00.000" + "\n");

            SqlParameter dateOut = new SqlParameter();
            dateOut.ParameterName = "@DateO_";
            dateOut.SqlDbType = SqlDbType.VarChar;
            dateOut.Direction = ParameterDirection.Input;
            dateOut.Value = dateOut_ + " 12:00:00.000";
            sqlCommand.Parameters.Add(dateOut);
            Debug.WriteLine(dateOut_ + " 12:00:00.000" + "\n");

            SqlParameter reservationNumber = new SqlParameter();
            reservationNumber.ParameterName = "@ReservationNumber_";
            reservationNumber.SqlDbType = SqlDbType.Char;
            reservationNumber.Direction = ParameterDirection.Input;
            string identificadorUnico = Guid.NewGuid().ToString();
            reservationNumber.Value = identificadorUnico;
            sqlCommand.Parameters.Add(reservationNumber);

            Debug.WriteLine(identificadorUnico + "\n");

            SqlParameter client = new SqlParameter();
            client.ParameterName = "@Client_";
            client.SqlDbType = SqlDbType.Int;
            client.Direction = ParameterDirection.Input;
            client.Value = client_;
            sqlCommand.Parameters.Add(client);
            Debug.WriteLine(client_ + "\n");


            SqlParameter room = new SqlParameter();
            room.ParameterName = "@Room_";
            room.SqlDbType = SqlDbType.Int;
            room.Direction = ParameterDirection.Input;
            room.Value = room_;
            sqlCommand.Parameters.Add(room);
            Debug.WriteLine(room_ + "\n");


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@R_";
            parameter.SqlDbType = SqlDbType.Char;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = "2";
            sqlCommand.Parameters.Add(parameter);
            Debug.WriteLine(room_ + "\n");


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Reservation_Number";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Direction = ParameterDirection.Output;
            parameter2.Value = "z";
            sqlCommand.Parameters.Add(parameter2);
            //Debug.WriteLine("z" + "\n");

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            string returnValue = parameter.Value.ToString();
            string reserNumber = parameter2.Value.ToString();

            sqlCommand.Connection.Close();
            Debug.WriteLine(returnValue + "\n");

            // aca informe

            try
            {
                String msg = "Código: " + identificadorUnico + "\n" +
                    "Desde: " + dateIn_ + "\n" +
                    "Hasta: " + dateOut_ + "\n";
                MailMessage mnsj = new MailMessage();
                mnsj.Subject = "Reservacion Cuatrivago";
                mnsj.To.Add(new MailAddress(mail));

                mnsj.From = new MailAddress("cuatrivagoucr@gmail.com", "ucrcuatrivago");
                mnsj.Body = msg;
                if (subscription=="true") { serverMail.sendMail(mnsj); }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString() + "abcassssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
            }

            return returnValue;
        }
    }
}