using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CuatrivagoProjectAdmin.Context
{
    public class HotelContext
    {
        public Hotel getInformationHotel(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Hotel";
            SqlCommand cmdHotel = new SqlCommand(sqlStoredProcedure, connection);
            cmdHotel.CommandType = CommandType.StoredProcedure;
            cmdHotel.Parameters.Add(new SqlParameter("@Hotel", 1));
            cmdHotel.Connection.Open();
            cmdHotel.ExecuteNonQuery();
            SqlDataReader reader = cmdHotel.ExecuteReader();
            Hotel hotel = new Hotel();
            reader.Read();
            hotel.name = reader.GetString(0);
            hotel.homeInformation = reader.GetString(1);
            hotel.aboutUs = reader.GetString(2);
            hotel.address_ = reader.GetString(3);
            hotel.phone = reader.GetString(4);
            hotel.postMail = reader.GetString(5);
            hotel.email = reader.GetString(6);
            cmdHotel.Connection.Close();
            return hotel;
        }
        public void updateHotel(Hotel hotel, string conn)
        {

            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Update_Hotel";
            SqlCommand cmdHotel = new SqlCommand(sqlStoredProcedure, connection);
            cmdHotel.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter( "@ID",SqlDbType.Int);
            //id.ParameterName =;
            //id.SqlDbType = ;
            id.Direction = ParameterDirection.Input;
            id.Value = hotel.idHotel;
            cmdHotel.Parameters.Add(id);


            SqlParameter name = new SqlParameter("@Name",SqlDbType.Char,100);
            //name.ParameterName = ;
            //name.SqlDbType = ;
            name.Direction = ParameterDirection.Input;
            name.Value = hotel.name;
            cmdHotel.Parameters.Add(name);


            SqlParameter home = new SqlParameter("@HomeInformation",SqlDbType.Char,2000);
            //home.ParameterName = ;
            //home.SqlDbType = ;
            home.Direction = ParameterDirection.Input;
            home.Value = hotel.homeInformation;
            cmdHotel.Parameters.Add(home);


            SqlParameter about = new SqlParameter("@AboutUs",SqlDbType.Char,2000);
            //about.ParameterName = "";
            //about.SqlDbType = ;
            about.Direction = ParameterDirection.Input;
            about.Value = hotel.aboutUs;
            cmdHotel.Parameters.Add(about);


            SqlParameter add = new SqlParameter("@Address",SqlDbType.Char,500);
            //add.ParameterName = ;
            //add.SqlDbType = ;
            add.Direction = ParameterDirection.Input;
            add.Value = hotel.address_;
            cmdHotel.Parameters.Add(add);


            SqlParameter phone = new SqlParameter("@Phone",SqlDbType.Int);
            //phone.ParameterName = ;
            //phone.SqlDbType = ;
            phone.Direction = ParameterDirection.Input;
            phone.Value = hotel.phone;
            cmdHotel.Parameters.Add(phone);


            SqlParameter mail = new SqlParameter("@MailPost",SqlDbType.Char,50);
            //mail.ParameterName = ;
            //mail.SqlDbType = ;
            mail.Direction = ParameterDirection.Input;
            mail.Value = hotel.postMail;
            cmdHotel.Parameters.Add(mail);


            SqlParameter parameter = new SqlParameter("@Email", SqlDbType.Char,200);
            //parameter.ParameterName = ;
            //parameter.SqlDbType =;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = hotel.email;
            cmdHotel.Parameters.Add(parameter);

            SqlParameter parameter2 = new SqlParameter("@R",SqlDbType.Char,1);
            //parameter2.ParameterName = ;
            //parameter2.SqlDbType = ;
            parameter2.Direction = ParameterDirection.Output;

            cmdHotel.Parameters.Add(parameter2);
            cmdHotel.Connection.Open();
            cmdHotel.ExecuteNonQuery();

            cmdHotel.Connection.Close();

        }
    }
}