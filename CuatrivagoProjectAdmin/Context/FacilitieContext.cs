using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class FacilitieContext
    {

        public int createFacilite(string conn, Facilitie facilitie)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Create_Facilitie";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Name", facilitie.Name));
            sqlCommand.Parameters.Add(new SqlParameter("@Description_", facilitie.Description));


            SqlParameter resultCreate = new SqlParameter("@R", "");
            resultCreate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultCreate);

            SqlParameter idNewFacilitie = new SqlParameter("@Facilitie", 0);
            idNewFacilitie.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(idNewFacilitie);



            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();
            int idNewFacilitie2 = Int32.Parse(sqlCommand.Parameters["@Facilitie"].Value.ToString());

            if (result.Equals("y"))
            {
                return idNewFacilitie2;
            }
            else
            {
                return -1;
            }

        }


        public List<Facilitie> getFacilities(string conn)
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

        public void deleteFacilitie(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Delete_Facilitie";
            SqlCommand cmdAdmin = new SqlCommand(sqlStoredProcedure, connection);

            cmdAdmin.CommandType = CommandType.StoredProcedure;

            //Se agregan los parametros
            cmdAdmin.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            cmdAdmin.Connection.Open();
            cmdAdmin.ExecuteNonQuery();
            cmdAdmin.Connection.Close();

        }

    }
}