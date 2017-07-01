using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CuatrivagoProject.Context
{
    public class HotelContext
    {
        public Hotel getInformationHotel(string conn) {
            
            SqlConnection connection = new SqlConnection(conn);



            string sqlStoredProcedure = "SP_Retrieve_Hotel";
            SqlCommand cmdHotel = new SqlCommand(sqlStoredProcedure, connection);

            cmdHotel.CommandType = CommandType.StoredProcedure;

            cmdHotel.Parameters.Add(new SqlParameter("@Hotel",1));

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

        public List<Image> getAllImageHome(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "sp_get_images_home";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Image> imageList = new List<Image>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Image image = new Image();
                    image.path = reader.GetString(0);
                    imageList.Add(image);
                }
            }
            sqlCommand.Connection.Close();
            return imageList;
        }

    }
}