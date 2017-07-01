using CuatrivagoProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CuatrivagoProject.Context
{
    public class RoomContext
    {
        string connUCR = WebConfigurationManager.ConnectionStrings["connectionUCR"].ToString();
        public RoomContext() { }
        // retorna los que estan disponibles por tipo, fechas ingreso y salida
        public List<Room> getRoomsAvailable(string conn, string arrival, string departure, int roomType)
        {
            SqlConnection connection = new SqlConnection(conn);
            // llamar procedimiento que retorne los disponibles
            string sqlStoredProcedure = "SP_Verify_Available_Rooms";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            string temp = arrival + " 14:00:00.000";
            string temp2 = departure + " 12:00:0.000";
            sqlCommand.Parameters.AddWithValue("@DateIn", temp);
            sqlCommand.Parameters.AddWithValue("@DateOut", temp2);
            sqlCommand.Parameters.AddWithValue("@RoomType", roomType);
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Room> roomList = new List<Room>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Room room = new Room();
                    room.idRoom = reader.GetInt32(0);
                    room.capacity = reader.GetInt32(1);
                    room.description_ = reader.GetString(2);
                    room.roomType = reader.GetInt32(3);
                    ImageContext context = new ImageContext();
                    room.images = context.getImageByType(connUCR, room.idRoom, 'R');
                    roomList.Add(room);
                }
            }
            sqlCommand.Connection.Close();
            return roomList;
        }
        //probar estooooooooooooooooooooooooooooooo
        public Room getRoom(string conn, int id)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Room";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Id", id);


            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();


            reader.Read();
            Room room = new Room();
            room.idRoom = reader.GetInt32(0);
            room.capacity = reader.GetInt32(1);
            room.description_ = reader.GetString(2);
            room.roomType = reader.GetInt32(3);
            ImageContext context = new ImageContext();
            room.images = context.getImageByType(connUCR, room.idRoom, 'R');
            sqlCommand.Connection.Close();
            return room;
        }


        public List<Room> getRoomsByType(string conn, int roomType, string colon, string dolar)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_All_Room_By_Room_Type";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@RoomType", roomType);
            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Room> roomList = new List<Room>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Room room = new Room();
                    room.idRoom = reader.GetInt32(0);
                    room.capacity = reader.GetInt32(1);
                    room.description_ = reader.GetString(2);
                    room.roomType = reader.GetInt32(3);
                    room.colon = colon;
                    room.dolar = dolar;
                    ImageContext context = new ImageContext();
                    room.images = context.getImageByType(connUCR, room.idRoom, 'R');
                    roomList.Add(room);
                }
            }
            sqlCommand.Connection.Close();
            return roomList;
        }
        public int verifyRoomAvailable(string conn, string dateIn_, string dateOut_, int roomType)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Verify_Available_Room";

            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter dateIn = new SqlParameter();
            dateIn.ParameterName = "@DateIn";
            dateIn.SqlDbType = SqlDbType.DateTime;
            dateIn.Direction = ParameterDirection.Input;
            dateIn.Value = dateIn_ + " 02:02:02";
            sqlCommand.Parameters.Add(dateIn);

            SqlParameter dateOut = new SqlParameter();
            dateOut.ParameterName = "@DateOut";
            dateOut.SqlDbType = SqlDbType.DateTime;
            dateOut.Direction = ParameterDirection.Input;
            dateOut.Value = dateOut_ + " 02:02:02";
            sqlCommand.Parameters.Add(dateOut);

            SqlParameter room = new SqlParameter();
            room.ParameterName = "@Room";
            room.SqlDbType = SqlDbType.VarChar;
            room.Direction = ParameterDirection.Input;
            room.Value = roomType;
            sqlCommand.Parameters.Add(room);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Available";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = 0;
            sqlCommand.Parameters.Add(parameter);

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();
            int returnValue = int.Parse(parameter.Value.ToString());
            sqlCommand.Connection.Close();
            return returnValue;
        }
    }
}