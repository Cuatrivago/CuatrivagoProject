using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class AdvertisementContext
    {


        public List<Advertisement> getInformationAdvertisement(string conn)
        {


            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_Advertisement_All_ID";
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
                    e_advertisement.idAdvertisement = reader.GetInt32(0);
                    e_advertisement.name = reader.GetString(1);
                    e_advertisement.description = reader.GetString(2);
                    e_advertisement.url = reader.GetString(3);
                    

                    advertisement.Add(e_advertisement);

                }
            }

            cmdAdvertisement.Connection.Close();

            return advertisement;


        }


        public int createAdvertisement(string conn, Advertisement advertisement)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Create_Advertisement";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Name", advertisement.name));
            sqlCommand.Parameters.Add(new SqlParameter("@Description_", advertisement.description));
            sqlCommand.Parameters.Add(new SqlParameter("@Url", advertisement.url));

            SqlParameter resultCreate = new SqlParameter("@R", "");
            resultCreate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultCreate);

            SqlParameter idNewAdvertisement = new SqlParameter("@Advertisement", 0);
            idNewAdvertisement.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(idNewAdvertisement);



            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();
            int idNewAdvertisement2 = Int32.Parse(sqlCommand.Parameters["@Advertisement"].Value.ToString());

            if (result.Equals("y"))
            {
                return idNewAdvertisement2;
            }
            else
            {
                return -1;
            }

        }


        public int deleteAdvertisementById(string conn, int id)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Delete_Advertisement";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", id));

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