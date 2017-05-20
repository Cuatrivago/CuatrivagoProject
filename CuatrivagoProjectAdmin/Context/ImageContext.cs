using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CuatrivagoProjectAdmin.Models;
using System.Data.SqlClient;
using System.Data;

namespace CuatrivagoProjectAdmin.Context
{
    public class ImageContext
    {
        public char updateImage()
        {
            return 'n';
        }
        
        public List<Image> getImage(String conn, int Room)
        {
            SqlConnection connection = new SqlConnection(conn);
            // llamar procedimiento que retorne los disponibles
            string sqlStoredProcedure = "SP_Retrieve_Image_By_Object";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Object", SqlDbType.Int).Value = Room;
            sqlCommand.Parameters.Add("@Type", SqlDbType.Char, size: 1).Value = 'R';

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            

            List <Image> ima = new List<Image>();

            if (reader.HasRows)
            {
                while(reader.Read()){
                    Image ge = new Image();
                    ge.description = reader.GetString(0);
                    ge.path = reader.GetString(1);
                    ima.Add(ge);
                }
            } else
            {
                Image ge = new Image();
                ge.description = "No hay imagenes asociadas a este valor, esto es un error";
                ima.Add(ge);
            }
            sqlCommand.Connection.Close();

            return ima;

        }
    }
}