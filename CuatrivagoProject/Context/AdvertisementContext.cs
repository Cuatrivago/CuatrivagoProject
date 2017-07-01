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
    public class AdvertisementContext
    {
        public List<Advertisement> getInformationAdvertisement(string conn)
        {


            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_Advertisement_All";
            SqlCommand cmdAdvertisement = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdvertisement.CommandType = CommandType.StoredProcedure;

            cmdAdvertisement.Connection.Open();
            cmdAdvertisement.ExecuteNonQuery();

            SqlDataReader reader = cmdAdvertisement.ExecuteReader();

            List<Advertisement> advertisement = new List<Advertisement>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Advertisement e_advertisement = new Advertisement();

                    e_advertisement.name = reader.GetString(0);
                    e_advertisement.description = reader.GetString(1);
                    e_advertisement.url = reader.GetString(2);
                    e_advertisement.path = reader.GetString(3);

                    advertisement.Add(e_advertisement);
                  
                }
            }

            cmdAdvertisement.Connection.Close();

            return advertisement;


        }
    }
}