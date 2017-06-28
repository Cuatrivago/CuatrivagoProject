using CuatrivagoProjectAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CuatrivagoProjectAdmin.Context
{
    public class UpdateRoomContext
    {


        public List<Room> getAllRommByType(string conn, int idTypeRoom)
        {
            SqlConnection connection = new SqlConnection(conn);
            string sqlStoredProcedure = "SP_Retrieve_All_Room_By_Room_Type_With_Price";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@RoomType", idTypeRoom));

            sqlCommand.Connection.Open();
            sqlCommand.ExecuteNonQuery();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            cr.fi.bccr.indicadoreseconomicos.wsIndicadoresEconomicos client = new cr.fi.bccr.indicadoreseconomicos.wsIndicadoresEconomicos();
            DataSet typeChange = client.ObtenerIndicadoresEconomicos("317", DateTime.Today.ToString("d"), DateTime.Today.ToString("d"), "Cuatrivago", "n");

            double colon = double.Parse(typeChange.Tables[0].Rows[0].ItemArray[2].ToString());

            List<Room> roomList = new List<Room>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Room room = new Room();
                    room.idRoom = reader.GetInt32(0);
                    room.capacity = reader.GetInt32(1);
                    room.description_ = reader.GetString(2);
                    room.priceDolar = "$" + reader.GetDouble(3).ToString();
                    room.priceColon = "₡" + (reader.GetDouble(3) * colon);
                    room.roomType = idTypeRoom;
                    roomList.Add(room);
                }
            }
            sqlCommand.Connection.Close();
            return roomList;
        }


        public int updateRoomByType(string conn, Room room)
        {

            SqlConnection connection = new SqlConnection(conn);

            string sqlStoredProcedure = "SP_Update_Room";
            SqlCommand sqlCommand = new SqlCommand(sqlStoredProcedure, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@ID", room.idRoom));
            sqlCommand.Parameters.Add(new SqlParameter("@Capacity", room.capacity));
            sqlCommand.Parameters.Add(new SqlParameter("@Description", room.description_));          
            //sqlCommand.Parameters.Add(new SqlParameter("@Price", room.price));


            SqlParameter resultUpdate = new SqlParameter("@R", "");
            resultUpdate.Direction = ParameterDirection.Output;
            sqlCommand.Parameters.Add(resultUpdate);

            
            sqlCommand.Connection.Open();

            sqlCommand.ExecuteNonQuery();

            string result = sqlCommand.Parameters["@R"].Value.ToString();

            if (result.Equals("y"))
            {
                return room.roomType;
            }
            else {
                return -1;
            }
            
        }
    }
}