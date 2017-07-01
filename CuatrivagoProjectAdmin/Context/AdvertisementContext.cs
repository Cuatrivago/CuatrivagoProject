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
    }
}