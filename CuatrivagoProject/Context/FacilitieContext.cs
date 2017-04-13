using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProject.Context
{
    public class FacilitieContext
    {
        public List<Facilitie> getAllFacilities(string conn)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_Facilitie_All";
            SqlCommand cmdFacilitie = new SqlCommand(sqlStoredProcedure, connection);
            
            cmdFacilitie.CommandType = CommandType.StoredProcedure;

            cmdFacilitie.Connection.Open();
            cmdFacilitie.ExecuteNonQuery();

            SqlDataReader reader = cmdFacilitie.ExecuteReader();

            List<Facilitie> facilities = new List<Facilitie>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Facilitie facilitie = new Facilitie();
                    facilitie.idFacilitie = reader.GetString(0);
                    facilitie.name = reader.GetString(1);
                    facilitie.description_ = reader.GetString(2);
                    facilitie.path = reader.GetString(3);

                    facilities.Add(facilitie);
                }
            }

            cmdFacilitie.Connection.Close();

            return facilities;

        }
    }
}