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
    public class AdminContext
    {

        public Admin logAdmin(string conn, Admin admint)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Verify_Admin";
            SqlCommand cmdAdvertisement = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdvertisement.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdvertisement.Parameters.Add("@Email", SqlDbType.Char, size: 200 ).Value = admint.email;
            cmdAdvertisement.Parameters.Add("@Password", SqlDbType.Char, size: 100).Value = admint.password;

            //Parametro de retorno
            SqlParameter returnedId = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmdAdvertisement.Parameters.Add(returnedId);


            cmdAdvertisement.Connection.Open();
            cmdAdvertisement.ExecuteNonQuery();

            Admin admin = new Admin();
            
            admin.idAdmin = int.Parse(returnedId.Value.ToString());

            cmdAdvertisement.Connection.Close();
            
            return admin;


        }

    }
}