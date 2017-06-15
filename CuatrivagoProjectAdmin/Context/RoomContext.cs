using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace CuatrivagoProjectAdmin.Context
{
    public class RoomContext
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

        public List<RangeDateRoom> getInformationRangeDateRoom(string conn, DateTime dateIn, DateTime dateOut, int type)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_VERIFY_ROOMS_USER";
            SqlCommand cmdRangeRoom = new SqlCommand(sqlStoredProcedure, connection);

            cmdRangeRoom.CommandType = CommandType.StoredProcedure;

            SqlParameter currentDateIn = new SqlParameter();
            currentDateIn.ParameterName = "@dateIn";
            currentDateIn.SqlDbType = SqlDbType.Date;
            currentDateIn.Direction = ParameterDirection.Input;
            currentDateIn.Value = dateIn;

            SqlParameter currentDateOut = new SqlParameter();
            currentDateOut.ParameterName = "@dateOut";
            currentDateOut.SqlDbType = SqlDbType.Date;
            currentDateOut.Direction = ParameterDirection.Input;
            currentDateOut.Value = dateOut;

            SqlParameter currentType = new SqlParameter();
            currentType.ParameterName = "@roomType";
            currentType.SqlDbType = SqlDbType.Int;
            currentType.Direction = ParameterDirection.Input;
            currentType.Value = type;


            cmdRangeRoom.Parameters.Add(currentDateIn);
            cmdRangeRoom.Parameters.Add(currentDateOut);
            cmdRangeRoom.Parameters.Add(currentType);


            cmdRangeRoom.Connection.Open();

            cmdRangeRoom.ExecuteNonQuery();


            SqlDataReader reader = cmdRangeRoom.ExecuteReader();

            List<RangeDateRoom> rooms = new List<RangeDateRoom>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RangeDateRoom room = new RangeDateRoom();
                    room.id = reader.GetInt32(0).ToString();
                    room.description = reader.GetString(1);
                    room.capacity = reader.GetInt32(2).ToString();
                    room.available = reader.GetString(3);
                    room.type = reader.GetString(4);
                    room.price = reader.GetInt32(5).ToString();
                    rooms.Add(room);
                }
            }

            cmdRangeRoom.Connection.Close();

            return rooms;
        }

        public List<RoomType> getAllRoomType(string conn)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Retrieve_Room_Type_Combo_Box";
            SqlCommand cmdRoomType = new SqlCommand(sqlStoredProcedure, connection);

            cmdRoomType.CommandType = CommandType.StoredProcedure;

            cmdRoomType.Connection.Open();
            cmdRoomType.ExecuteNonQuery();

            SqlDataReader reader = cmdRoomType.ExecuteReader();

            List<RoomType> allRoomType = new List<RoomType>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RoomType roomType = new RoomType();
                    roomType.id = reader.GetInt32(0).ToString();
                    roomType.type = reader.GetString(1);

                    allRoomType.Add(roomType);
                }
            }

            cmdRoomType.Connection.Close();

            return allRoomType;

        }

        public List<Room> getRoomComboBox(string conn)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_Room_ComboBox";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
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
                    room.description_ = reader.GetString(1);
                    roomList.Add(room);
                }
            }
            sqlCommand.Connection.Close();
            return roomList;
        }


        public int createRoom(string conn, Room room)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Create_Room";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@Capacity", room.capacity));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", room.description_));
            sqlCommand.Parameters.Add(new SqlParameter("@RoomType", room.roomType));


            SqlParameter resultCreate = new SqlParameter("@R", "");
            resultCreate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultCreate);

            SqlParameter idNewRoom = new SqlParameter("@Room",0);
            idNewRoom.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(idNewRoom);



            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();
            int idNewRoom2 = Int32.Parse(sqlCommand.Parameters["@Room"].Value.ToString());

            if (result.Equals("y"))
            {
                return idNewRoom2;
            }
            else
            {
                return -1;
            }

        }




    }
}