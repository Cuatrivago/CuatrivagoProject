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
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Email", SqlDbType.Char, size: 200 ).Value = admint.email;
            cmdAdmin.Parameters.Add("@Password", SqlDbType.Char, size: 100).Value = admint.password;

            //Parametro de retorno
            SqlParameter returnedId = new SqlParameter("@Return", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedId);

            SqlParameter returnedRol = new SqlParameter("@Rol", SqlDbType.Int) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedRol);

            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();

            Admin admin = new Admin();
            
            admin.idAdmin = int.Parse(returnedId.Value.ToString());
            admin.rol = returnedRol.Value.ToString();

            cmdAdmin.Connection.Close();
            
            return admin;
        }

        public char createAdmin(string conn, Admin admin)
        {
            SqlConnection connection = new SqlConnection(conn);
            char retur;
            string sqlStoredProcedure = "SP_Create_Admin";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Name", SqlDbType.Char, size: 100).Value = admin.name;
            cmdAdmin.Parameters.Add("@LastName", SqlDbType.Char, size: 100).Value = admin.lastName;
            cmdAdmin.Parameters.Add("@Email", SqlDbType.Char, size: 200).Value = admin.email;
            cmdAdmin.Parameters.Add("@Password", SqlDbType.Char, size: 100).Value = admin.password;

            //Parametro de retorno
            SqlParameter returnedChar = new SqlParameter("@R", SqlDbType.Char, size:1) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedChar);


            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();

            retur = Char.Parse(returnedChar.Value.ToString());

            cmdAdmin.Connection.Close();

            return retur;
        }

        public char updateAdmin(string conn, Admin admin)
        {
            SqlConnection connection = new SqlConnection(conn);
            char retur;
            string sqlStoredProcedure = "";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Name", SqlDbType.Char, size: 100).Value = admin.name;
            cmdAdmin.Parameters.Add("@LastName", SqlDbType.Char, size: 100).Value = admin.lastName;
            cmdAdmin.Parameters.Add("@Email", SqlDbType.Char, size: 200).Value = admin.email;

            //Parametro de retorno
            SqlParameter returnedChar = new SqlParameter("@R", SqlDbType.Char, size: 1) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedChar);


            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();

            retur = Char.Parse(returnedChar.Value.ToString());

            cmdAdmin.Connection.Close();

            return retur;
        }

        public char updatePassAdmin(string conn, string oldPass, string newPass)
        {
            SqlConnection connection = new SqlConnection(conn);
            char retur;
            string sqlStoredProcedure = "";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Oldpass", SqlDbType.Char, size: 100).Value = oldPass;
            cmdAdmin.Parameters.Add("@Newpass", SqlDbType.Char, size: 100).Value = newPass;

            //Parametro de retorno
            SqlParameter returnedChar = new SqlParameter("@R", SqlDbType.Char, size: 1) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedChar);


            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();

            retur = Char.Parse(returnedChar.Value.ToString());

            cmdAdmin.Connection.Close();

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

        public List<Admin> getAllAdmins(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            cmdAdmin.Connection.Open();

            cmdAdmin.ExecuteNonQuery();

            SqlDataReader reader = cmdAdmin.ExecuteReader();

            List<Admin> listAdmins = new List<Admin>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Admin admin = new Admin();
                    admin.idAdmin = reader.GetInt32(0);
                    admin.name = reader.GetString(1);
                    admin.lastName = reader.GetString(2);
                    admin.email = reader.GetString(3);
                    admin.password = reader.GetString(4);
                    admin.rol = reader.GetString(5);
                    listAdmins.Add(admin);
                }
            }

            cmdAdmin.Connection.Close();

            return listAdmins;
        }

        public List<Admin> getAdminById(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            SqlParameter currentId = new SqlParameter();
            currentId.ParameterName = "@id";
            currentId.SqlDbType = SqlDbType.Int;
            currentId.Direction = ParameterDirection.Input;
            currentId.Value = id;

            cmdAdmin.Parameters.Add(currentId);

            cmdAdmin.Connection.Open();

            cmdAdmin.ExecuteNonQuery();

            SqlDataReader reader = cmdAdmin.ExecuteReader();

            List<Admin> listAdmins = new List<Admin>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Admin admin = new Admin();
                    admin.idAdmin = reader.GetInt32(0);
                    admin.name = reader.GetString(1);
                    admin.lastName = reader.GetString(2);
                    admin.email = reader.GetString(3);
                    admin.password = reader.GetString(4);
                    admin.rol = reader.GetString(5);
                    listAdmins.Add(admin);
                }
            }

            cmdAdmin.Connection.Close();

            return listAdmins;
        }

        public void deleteAdmin(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            //Parametro de retorno
            SqlParameter returnedChar = new SqlParameter("@R", SqlDbType.Char, size: 1) { Direction = ParameterDirection.Output };
            cmdAdmin.Parameters.Add(returnedChar);

            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();
            cmdAdmin.Connection.Close();

        }

    }
}