using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace CuatrivagoProjectAdmin.Context
{
    public class Room
    {

        public List<StateRoom> getInformationStateRoomToday(string conn, DateTime date)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Verify_Available_Room_Today";
            SqlCommand cmdStateRoom = new SqlCommand(sqlStoredProcedure, connection);

            cmdStateRoom.CommandType = CommandType.StoredProcedure;

            SqlParameter currentDate = new SqlParameter();
            currentDate.ParameterName = "@Today";
            currentDate.SqlDbType = SqlDbType.Date;
            currentDate.Direction = ParameterDirection.Input;
            currentDate.Value = date;
            cmdStateRoom.Parameters.Add(currentDate);


            cmdStateRoom.Connection.Open();


            cmdStateRoom.ExecuteNonQuery();


            SqlDataReader reader = cmdStateRoom.ExecuteReader();

            List<StateRoom> rooms = new List<StateRoom>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StateRoom room = new StateRoom();
                    room.nameRoom = reader.GetString(0);
                    room.state = reader.GetString(1);                  

                    rooms.Add(room);
                }
            }

            cmdStateRoom.Connection.Close();

            return rooms;



        }

    }
}