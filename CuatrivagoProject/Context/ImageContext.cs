using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Context
{
    public class ImageContext
    {
        public List<Image> getAllImage(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Image_Gallery";
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
                    image.id = Int32.Parse(reader.GetString(0));
                    image.description = reader.GetString(1);
                    image.path = reader.GetString(2);
                    imageList.Add(image);
                }
            }
            sqlCommand.Connection.Close();
            return imageList;
        }

      
    }
}