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
        
        
        public List<Image> getImagesByObject(String conn, int idObject, char type)
        {
            SqlConnection connection = new SqlConnection(conn);
            // llamar procedimiento que retorne los disponibles
            string sqlStoredProcedure = "SP_Retrieve_Image_By_Object";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Object", SqlDbType.Int).Value = idObject;
            sqlCommand.Parameters.Add("@Type", SqlDbType.Char, size: 1).Value = type;

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
                    ge.idImage = reader.GetInt32(2);
                    ge.idObject = idObject;
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

        public int createImageByObject(string conn, Image image)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Create_Image_UCR";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Description", image.description));
            sqlCommand.Parameters.Add(new SqlParameter("@Image_", image.path));


            SqlParameter resultUpdate = new SqlParameter("@R", "");
            resultUpdate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultUpdate);

            SqlParameter idImage = new SqlParameter("@Image",0);
            idImage.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(idImage);


            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();
            int id = Int32.Parse(sqlCommand.Parameters["@Image"].Value.ToString());

            if (result.Equals("y"))
            {
                return id;
            }
            else
            {
                return -1;
            }

        }

        public int asociateImageObject(string conn, char object_, int objectId, int idImage)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Asociate_Image_Object";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Object", object_));
            sqlCommand.Parameters.Add(new SqlParameter("@ImageId", idImage));
            sqlCommand.Parameters.Add(new SqlParameter("@ObjectId", objectId));

            SqlParameter resultUpdate = new SqlParameter("@R", "");
            resultUpdate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultUpdate);
                     


            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();

            if (result.Equals("y"))
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public int deleteImageById(string conn, int idImage)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Delete_Image_UCR";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;          
            sqlCommand.Parameters.Add(new SqlParameter("@idImage", idImage));

            SqlParameter resultUpdate = new SqlParameter("@R", "");
            resultUpdate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultUpdate);



            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();

            if (result.Equals("y"))
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }


    }
}