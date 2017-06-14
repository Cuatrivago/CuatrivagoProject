using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class UpdateFacilitieContext
    {
        public List<Facilitie> getFacilitieCombo(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Facilitie_Combo_Box";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Facilitie> roomList = new List<Facilitie>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Facilitie facilitie = new Facilitie();
                    facilitie.IdFacilitie = reader.GetInt32(0);
                    facilitie.Name = reader.GetString(1);
                    roomList.Add(facilitie);
                }
            }
            sqlCommand.Connection.Close();
            return roomList;
        }

        public Facilitie getFacilitie(Facilitie fas, string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Facilitie";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@Facilitie", SqlDbType.Int).Value = fas.IdFacilitie;

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                fas.Name = reader.GetString(0);
                fas.Description = reader.GetString(1);
            }
            else
            {
                fas.IdFacilitie = -1;
                fas.Name = "No facilitie found";
                fas.Description = "No facilitie description";
            }
            sqlCommand.Connection.Close();
            return fas;
        }

        public char updateFacilitie(Facilitie fas, string conn)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Update_Facilitie";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = fas.IdFacilitie;
            sqlCommand.Parameters.Add("@Description", SqlDbType.Char, size: 1000).Value = fas.Description;
            sqlCommand.Parameters.Add("@Name", SqlDbType.Char, size: 100).Value = fas.Name;


            SqlParameter resultUpdate = new SqlParameter("@R", " ");
            resultUpdate.Direction = ParameterDirection.Output;
            resultUpdate.SqlDbType = SqlDbType.Char;
            resultUpdate.Size = 1;
            sqlCommand.Parameters.Add(resultUpdate);


            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();

            return result[0];

        }
    }
}