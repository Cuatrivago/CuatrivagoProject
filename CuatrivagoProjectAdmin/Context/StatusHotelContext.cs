using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class StatusHotelContext
    {
        public List<StatusHotel> getStatustHotel(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Hotel_Status_Report";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@today", SqlDbType.Date).Value = DateTime.Today;

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<StatusHotel> statusList = new List<StatusHotel>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StatusHotel status = new StatusHotel();
                    status.name = reader.GetString(0);
                    status.quantity = reader.GetInt32(1);
                    status.reserve = reader.GetInt32(2);
                    status.blocked = reader.GetInt32(3);
                    statusList.Add(status);
                }
            }
            sqlCommand.Connection.Close();
            return statusList;
        }
    }
}