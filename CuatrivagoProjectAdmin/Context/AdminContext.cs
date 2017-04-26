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

        public char createAdmin(string conn, Admin admin)
        {
            SqlConnection connection = new SqlConnection(conn);
            char retur;
            string sqlStoredProcedure = "SP_Create_Admin";
            SqlCommand cmdAdvertisement = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdvertisement.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdvertisement.Parameters.Add("@Name", SqlDbType.Char, size: 100).Value = admin.name;
            cmdAdvertisement.Parameters.Add("@LastName", SqlDbType.Char, size: 100).Value = admin.lastName;
            cmdAdvertisement.Parameters.Add("@Email", SqlDbType.Char, size: 200).Value = admin.email;
            cmdAdvertisement.Parameters.Add("@Password", SqlDbType.Char, size: 100).Value = admin.password;

            //Parametro de retorno
            SqlParameter returnedChar = new SqlParameter("@R", SqlDbType.Char, size:1) { Direction = ParameterDirection.Output };
            cmdAdvertisement.Parameters.Add(returnedChar);


            cmdAdvertisement.Connection.Open();
            cmdAdvertisement.ExecuteNonQuery();

            retur = Char.Parse(returnedChar.Value.ToString());

            cmdAdvertisement.Connection.Close();

            return retur;
        }

        public Admin retrieveAdmin(String conn, Admin adm)
        {
            SqlConnection connection = new SqlConnection(conn);
            // llamar procedimiento que retorne los disponibles
            string sqlStoredProcedure = "SP_Retrieve_Admin";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Admin", SqlDbType.Int).Value = adm.idAdmin;

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            reader.Read();
            
                
            adm.name = reader.GetString(0);
            adm.lastName= reader.GetString(1);
            adm.email = reader.GetString(2);

            sqlCommand.Connection.Close();
            return adm;
        }

    }
}